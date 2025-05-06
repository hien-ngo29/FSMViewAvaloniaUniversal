using AssetsTools.NET;
using FSMExpress.Common.Document;
using FSMExpress.Common.Interfaces;
using FSMExpress.PlayMaker.Structs;
using System.Drawing;
using System.Text;
using DrawingColor = System.Drawing.Color;
using EngineColor = FSMExpress.PlayMaker.Structs.Color;

namespace FSMExpress.PlayMaker;
public class FsmPlaymaker : IFsmMonoBehaviour
{
    public int Version { get; set; } = -1;
    public string Name { get; set; } = "";
    public string GoName { get; set; } = "";

    public FsmState? StartState = null;
    public List<FsmState> States { get; } = [];
    public List<FsmEvent> Events { get; } = [];
    public List<FsmTransition> GlobalTransitions { get; } = [];
    public FsmVariables Variables { get; set; }

    public FsmPlaymaker(IAssetField field)
    {
        Version = field.GetValue<int>("dataVersion");
        Name = field.GetValue<string>("name");
        GoName = string.Empty; // needs filling from outside of constructor

        var startStateName = field.GetValue<string>("startState");
        States = field.GetValueArray("states", x => new FsmState(x));
        Events = field.GetValueArray("events", x => new FsmEvent(x));
        GlobalTransitions = field.GetValueArray("globalTransitions", x => new FsmTransition(x));
        Variables = new FsmVariables(field.GetField("variables"));
        StartState = States.Find(s => s.Name == startStateName);
    }

    public FsmDocument MakeDocument()
    {
        var stateLookup = new Dictionary<string, FsmDocumentNode>();
        var doc = new FsmDocument(Name);
        foreach (var state in States)
        {
            var docNode = new FsmDocumentNode(state.Name);
            doc.Nodes.Add(docNode);
            stateLookup[state.Name] = docNode;
        }

        foreach (var state in States)
        {
            var docNode = stateLookup[state.Name];
            docNode.IsStart = state == StartState;

            var statePos = state.Position;
            docNode.Bounds = new RectangleF(statePos.X, statePos.Y, statePos.Width, statePos.Height);

            var stateColor = state.ColorIndex;
            docNode.NodeColor = STATE_COLORS[stateColor];
            docNode.TransitionColor = TRANSITION_COLORS[stateColor];

            foreach (var transition in state.Transitions)
            {
                var docNodeTransition = new FsmDocumentNodeTransition(transition.Event.Name);
                if (stateLookup.TryGetValue(transition.ToState, out var toNode))
                {
                    docNodeTransition.ToNode = toNode;
                }

                docNode.Transitions.Add(docNodeTransition);
            }

            //docNode.Fields.Add(new FsmDocumentNodeClassField(new AssetTypeReference("Classname", "Namespace", "AssemblyName"), true));
            //docNode.Fields.Add(new FsmDocumentNodeDataField("abc", new FsmDocumentNodeFieldIntegerValue(123)));
            //docNode.Fields.Add(new FsmDocumentNodeDataField("def", new FsmDocumentNodeFieldStringValue("some string")));

            var stateActionData = state.ActionData;
            for (var actionIdx = 0; actionIdx < stateActionData.ActionNames.Count; actionIdx++)
            {
                var actionName = stateActionData.ActionNames[actionIdx];
                if (actionName.Contains('.'))
                    actionName = actionName[(actionName.LastIndexOf('.') + 1)..];

                docNode.Fields.Add(new FsmDocumentNodeClassField(new AssetTypeReference(actionName, "Namespace", "AssemblyName"), true));
                ConvertActionData(docNode.Fields, stateActionData, actionIdx);
            }
        }

        foreach (var transition in GlobalTransitions)
        {
            var toNode = stateLookup[transition.ToState];
            var docNode = new FsmDocumentNode(transition.Event.Name)
            {
                IsStart = false,
                IsGlobal = true,
                Bounds = new RectangleF(toNode.Bounds.X, toNode.Bounds.Y - 50, toNode.Bounds.Width, 18)
            };

            var docNodeTransition = new FsmDocumentNodeTransition(transition.Event.Name)
            {
                ToNode = toNode
            };

            docNode.Transitions.Add(docNodeTransition);
            doc.Nodes.Add(docNode);
        }

        return doc;
    }

    private object ConvertActionDataArray(FsmActionData actionData, ref int paramIdx)
    {
        var type = actionData.ArrayParamTypes[actionData.ParamDataPos[paramIdx]];
        var size = actionData.ArrayParamSizes[actionData.ParamDataPos[paramIdx]];

        var elements = new object[size];
        for (var eleIdx = 0; eleIdx < size; eleIdx++)
        {
            paramIdx++;
            elements[eleIdx] = ConvertFsmObject(actionData, ref paramIdx);
        }

        return new FsmArrayInfo(type, elements);
    }

    private void ConvertActionData(List<FsmDocumentNodeField> fields, FsmActionData actionData, int actionIdx)
    {
        var startIndex = actionData.ActionStartIndex[actionIdx];
        var endIndex = (actionIdx == actionData.ActionNames.Count - 1)
            ? actionData.ParamDataType.Count
            : actionData.ActionStartIndex[actionIdx + 1];

        for (var paramIdx = startIndex; paramIdx < endIndex; paramIdx++)
        {
            var paramName = actionData.ParamName[paramIdx];
            var paramObj = ConvertFsmObject(actionData, ref paramIdx);
            fields.Add(new FsmDocumentNodeDataField(paramName, ConvertObjectToNodeFieldValue(paramObj)));
            if (paramObj is FsmArrayInfo objArrayInf)
            {
                for (var eleIdx = 0; eleIdx < objArrayInf.Elements.Length; eleIdx++)
                {
                    var element = objArrayInf.Elements[eleIdx];
                    fields.Add(new FsmDocumentNodeDataField($"[{eleIdx}]", ConvertObjectToNodeFieldValue(element)));
                }
            }
        }
    }

    private object ConvertFsmObject(FsmActionData actionData, ref int paramIdx)
    {
        var paramDataType = actionData.ParamDataType[paramIdx];
        var paramDataPos = actionData.ParamDataPos[paramIdx];
        var paramByteDataSize = actionData.ParamByteDataSize[paramIdx];

        var r = new BinaryReader(new MemoryStream(actionData.ByteData));
        r.BaseStream.Position = paramDataPos;

        object ret = paramDataType switch
        {
            ParamDataType.Integer => r.ReadInt32(),
            ParamDataType.FsmInt when Version == 1 => new FsmInt() { Value = r.ReadInt32() },
            ParamDataType.Enum => r.ReadInt32(),
            ParamDataType.Boolean => r.ReadBoolean(),
            ParamDataType.FsmBool when Version == 1 => new FsmBool { Value = r.ReadBoolean() },
            ParamDataType.Float => r.ReadSingle(),
            ParamDataType.FsmFloat when Version == 1 => new FsmFloat { Value = r.ReadSingle() },
            ParamDataType.String => Encoding.UTF8.GetString(r.ReadBytes(paramByteDataSize)),
            ParamDataType.FsmEvent when Version == 1 => new FsmEvent { Name = Encoding.UTF8.GetString(r.ReadBytes(paramByteDataSize)) },
            ParamDataType.Vector2 => new Vector2 { X = r.ReadSingle(), Y = r.ReadSingle() },
            ParamDataType.FsmVector2 when Version == 1 => new FsmVector2 { Value = new Vector2 { X = r.ReadSingle(), Y = r.ReadSingle() } },
            ParamDataType.Vector3 => new Vector3 { X = r.ReadSingle(), Y = r.ReadSingle(), Z = r.ReadSingle() },
            ParamDataType.FsmVector3 when Version == 1 => new FsmVector3 { Value = new Vector3 { X = r.ReadSingle(), Y = r.ReadSingle(), Z = r.ReadSingle() } },
            ParamDataType.Quaternion => new Quaternion { X = r.ReadSingle(), Y = r.ReadSingle(), Z = r.ReadSingle(), W = r.ReadSingle() },
            ParamDataType.FsmQuaternion when Version == 1 => new FsmQuaternion { Value = new Quaternion { X = r.ReadSingle(), Y = r.ReadSingle(), Z = r.ReadSingle(), W = r.ReadSingle() } },
            ParamDataType.Color => new EngineColor { R = r.ReadSingle(), G = r.ReadSingle(), B = r.ReadSingle(), A = r.ReadSingle() },
            ParamDataType.FsmColor when Version == 1 => new FsmColor { Value = new EngineColor { R = r.ReadSingle(), G = r.ReadSingle(), B = r.ReadSingle(), A = r.ReadSingle() } },
            ParamDataType.Rect => new Rect { X = r.ReadSingle(), Y = r.ReadSingle(), Width = r.ReadSingle(), Height = r.ReadSingle() },
            ParamDataType.FsmRect when Version == 1 => new FsmRect { Value = new Rect { X = r.ReadSingle(), Y = r.ReadSingle(), Width = r.ReadSingle(), Height = r.ReadSingle() } },
            /////////////////////////////////////////////////////////

            ParamDataType.FsmBool when Version > 1 => actionData.FsmBoolParams[paramDataPos],
            ParamDataType.FsmInt when Version > 1 => actionData.FsmIntParams[paramDataPos],
            ParamDataType.FsmFloat when Version > 1 => actionData.FsmFloatParams[paramDataPos],
            ParamDataType.FsmVector2 when Version > 1 => actionData.FsmVector2Params[paramDataPos],
            ParamDataType.FsmVector3 when Version > 1 => actionData.FsmVector3Params[paramDataPos],
            ParamDataType.FsmQuaternion when Version > 1 => actionData.FsmQuaternionParams[paramDataPos],
            ParamDataType.FsmColor when Version > 1 => actionData.FsmColorParams[paramDataPos],
            ParamDataType.FsmRect when Version > 1 => actionData.FsmRectParams[paramDataPos],
            ///////////////////////////////////////////////////////// 
            ParamDataType.FsmEnum => actionData.FsmEnumParams[paramDataPos],
            ParamDataType.FsmGameObject => actionData.FsmGameObjectParams[paramDataPos],
            ParamDataType.FsmOwnerDefault => actionData.FsmOwnerDefaultParams[paramDataPos],
            ParamDataType.FsmObject => actionData.FsmObjectParams[paramDataPos],
            ParamDataType.FsmVar => actionData.FsmVarParams[paramDataPos],
            ParamDataType.FsmString => actionData.FsmStringParams[paramDataPos],
            ParamDataType.FsmEvent => actionData.StringParams[paramDataPos],
            ParamDataType.FsmEventTarget => actionData.FsmEventTargetParams[paramDataPos],
            ParamDataType.FsmArray => actionData.FsmArrayParams[paramDataPos],
            ParamDataType.ObjectReference => $"ObjRef([{actionData.UnityObjectParams[paramDataPos]}])",
            ParamDataType.FunctionCall => actionData.FunctionCallParams[paramDataPos],
            ParamDataType.Array => ConvertActionDataArray(actionData, ref paramIdx),
            ParamDataType.FsmProperty => actionData.FsmPropertyParams[paramDataPos],
            _ => $"[{paramDataType} not implemented]",
        };

        if (Version == 1 && ret is NamedVariable namedVar)
        {
            switch (paramDataType)
            {
                case ParamDataType.FsmInt:
                case ParamDataType.FsmBool:
                case ParamDataType.FsmFloat:
                case ParamDataType.FsmVector2:
                case ParamDataType.FsmVector3:
                case ParamDataType.FsmQuaternion:
                case ParamDataType.FsmColor:
                    namedVar.UseVariable = r.ReadBoolean();

                    var nameLength = paramByteDataSize - ((int)r.BaseStream.Position - paramDataPos);
                    namedVar.Name = Encoding.UTF8.GetString(r.ReadBytes(nameLength));
                    break;
            }
        }

        return ret;
    }

    private FsmDocumentNodeFieldValue ConvertObjectToNodeFieldValue(object obj)
    {
        if (obj is int objInt)
            return new FsmDocumentNodeFieldIntegerValue(objInt);
        else if (obj is float objFloat)
            return new FsmDocumentNodeFieldFloatValue(objFloat);
        else if (obj is bool objBool)
            return new FsmDocumentNodeFieldBooleanValue(objBool);
        else if (obj is string objString)
            return new FsmDocumentNodeFieldStringValue(objString);
        else if (obj is FsmArrayInfo objArrayInf)
            return new FsmDocumentNodeFieldArrayValue(objArrayInf.TypeName, objArrayInf.Elements.Length);
        else
            return new FsmDocumentNodeFieldObjectValue(obj);

        //return new FsmDocumentNodeFieldStringValue("[unsupported object]");
    }

    private static readonly DrawingColor[] STATE_COLORS =
    [
        DrawingColor.FromArgb(128, 128, 128),
        DrawingColor.FromArgb(116, 143, 201),
        DrawingColor.FromArgb(58, 182, 166),
        DrawingColor.FromArgb(93, 164, 53),
        DrawingColor.FromArgb(225, 254, 50),
        DrawingColor.FromArgb(235, 131, 46),
        DrawingColor.FromArgb(187, 75, 75),
        DrawingColor.FromArgb(117, 53, 164)
    ];

    private static readonly DrawingColor[] TRANSITION_COLORS =
    [
        DrawingColor.FromArgb(222, 222, 222),
        DrawingColor.FromArgb(197, 213, 248),
        DrawingColor.FromArgb(159, 225, 216),
        DrawingColor.FromArgb(183, 225, 159),
        DrawingColor.FromArgb(225, 254, 102),
        DrawingColor.FromArgb(255, 198, 152),
        DrawingColor.FromArgb(225, 159, 160),
        DrawingColor.FromArgb(197, 159, 225)
    ];

    private class FsmArrayInfo(string typeName, object[] elements)
    {
        public string TypeName = typeName;
        public object[] Elements = elements;
    }
}
