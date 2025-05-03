using FSMExpress.Common.Document;
using FSMExpress.Common.Interfaces;
using FSMExpress.PlayMaker.Structs;
using System.Drawing;
using Color = System.Drawing.Color;

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

            //var stateActionData = state.ActionData;
            //var stateActionFields = new List<FsmDocumentNodeField>();
            //for (var i = 0; i < stateActionData.ActionNames.Count; i++)
            //{
            //    ConvertActionData(stateActionFields, stateActionData, i);
            //}
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

    private void ConvertActionData(List<FsmDocumentNodeField> fields, FsmActionData actionData, int index)
    {
        var actionName = actionData.ActionNames[index];
        if (actionName.Contains('.'))
            actionName = actionName[(actionName.LastIndexOf('.') + 1)..];

        var startIndex = actionData.ActionStartIndex[index];
        var endIndex = (index == actionData.ActionNames.Count - 1)
            ? actionData.ParamDataType.Count
            : actionData.ActionStartIndex[index + 1];

        for (var i = startIndex; i < endIndex; i++)
        {
            var paramName = actionData.ParamName[i];
            var paramDataType = actionData.ParamDataType[index];
        }
    }

    private void GetObject()
    {
        // todo
    }

    private static readonly Color[] STATE_COLORS =
    [
        Color.FromArgb(128, 128, 128),
        Color.FromArgb(116, 143, 201),
        Color.FromArgb(58, 182, 166),
        Color.FromArgb(93, 164, 53),
        Color.FromArgb(225, 254, 50),
        Color.FromArgb(235, 131, 46),
        Color.FromArgb(187, 75, 75),
        Color.FromArgb(117, 53, 164)
    ];

    private static readonly Color[] TRANSITION_COLORS =
    [
        Color.FromArgb(222, 222, 222),
        Color.FromArgb(197, 213, 248),
        Color.FromArgb(159, 225, 216),
        Color.FromArgb(183, 225, 159),
        Color.FromArgb(225, 254, 102),
        Color.FromArgb(255, 198, 152),
        Color.FromArgb(225, 159, 160),
        Color.FromArgb(197, 159, 225)
    ];
}
