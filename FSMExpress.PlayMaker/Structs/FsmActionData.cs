using FSMExpress.Common.Assets;
using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmActionData
{
    public List<string> ActionNames { get; set; }
    public List<string> CustomNames { get; set; }
    public List<bool> ActionEnabled { get; set; }
    public List<bool> ActionIsOpen { get; set; }
    public List<int> ActionStartIndex { get; set; }
    public List<int> ActionHashCodes { get; set; }
    public List<NamedAssetPPtr> UnityObjectParams { get; set; }
    public List<FsmGameObject> FsmGameObjectParams { get; set; }
    public List<FsmOwnerDefault> FsmOwnerDefaultParams { get; set; }
    public List<FsmAnimationCurve> AnimationCurveParams { get; set; }
    public List<FsmFunctionCall> FunctionCallParams { get; set; }
    public List<FsmTemplateControl> FsmTemplateControlParams { get; set; }
    public List<FsmEventTarget> FsmEventTargetParams { get; set; }
    public List<FsmProperty> FsmPropertyParams { get; set; }
    public List<FsmLayoutOption> LayoutOptionParams { get; set; }
    public List<FsmString> FsmStringParams { get; set; }
    public List<FsmObject> FsmObjectParams { get; set; }
    public List<FsmVar> FsmVarParams { get; set; }
    public List<FsmArray> FsmArrayParams { get; set; }
    public List<FsmEnum> FsmEnumParams { get; set; }
    public List<FsmFloat> FsmFloatParams { get; set; }
    public List<FsmInt> FsmIntParams { get; set; }
    public List<FsmBool> FsmBoolParams { get; set; }
    public List<FsmVector2> FsmVector2Params { get; set; }
    public List<FsmVector3> FsmVector3Params { get; set; }
    public List<FsmColor> FsmColorParams { get; set; }
    public List<FsmRect> FsmRectParams { get; set; }
    public List<FsmQuaternion> FsmQuaternionParams { get; set; }
    public List<string> StringParams { get; set; }
    public byte[] ByteData { get; set; }
    public List<int> ArrayParamSizes { get; set; }
    public List<string> ArrayParamTypes { get; set; }
    public List<int> CustomTypeSizes { get; set; }
    public List<string> CustomTypeNames { get; set; }
    public List<ParamDataType> ParamDataType { get; set; }
    public List<string> ParamName { get; set; }
    public List<int> ParamDataPos { get; set; }
    public List<int> ParamByteDataSize { get; set; }

    public FsmActionData()
    {
        ActionNames = [];
        CustomNames = [];
        ActionEnabled = [];
        ActionIsOpen = [];
        ActionStartIndex = [];
        ActionHashCodes = [];
        UnityObjectParams = [];
        FsmGameObjectParams = [];
        FsmOwnerDefaultParams = [];
        AnimationCurveParams = [];
        FunctionCallParams = [];
        FsmTemplateControlParams = [];
        FsmEventTargetParams = [];
        FsmPropertyParams = [];
        LayoutOptionParams = [];
        FsmStringParams = [];
        FsmObjectParams = [];
        FsmVarParams = [];
        FsmArrayParams = [];
        FsmEnumParams = [];
        FsmFloatParams = [];
        FsmIntParams = [];
        FsmBoolParams = [];
        FsmVector2Params = [];
        FsmVector3Params = [];
        FsmColorParams = [];
        FsmRectParams = [];
        FsmQuaternionParams = [];
        StringParams = [];
        ByteData = [];
        ArrayParamSizes = [];
        ArrayParamTypes = [];
        CustomTypeSizes = [];
        CustomTypeNames = [];
        ParamDataType = [];
        ParamName = [];
        ParamDataPos = [];
        ParamByteDataSize = [];
    }

    public FsmActionData(IAssetField field)
    {
        ActionNames = field.GetValue<List<string>>("actionNames");
        CustomNames = field.GetValue<List<string>>("customNames");
        ActionEnabled = field.GetValue<List<bool>>("actionEnabled");
        ActionIsOpen = field.GetValue<List<bool>>("actionIsOpen");
        ActionStartIndex = field.GetValue<List<int>>("actionStartIndex");
        ActionHashCodes = field.GetValue<List<int>>("actionHashCodes");
        UnityObjectParams = field.GetValue<List<NamedAssetPPtr>>("unityObjectParams");
        FsmGameObjectParams = field.GetValueArray("fsmGameObjectParams", x => new FsmGameObject(x));
        FsmOwnerDefaultParams = field.GetValueArray("fsmOwnerDefaultParams", x => new FsmOwnerDefault(x));
        AnimationCurveParams = field.GetValueArray("animationCurveParams", x => new FsmAnimationCurve(x));
        FunctionCallParams = field.GetValueArray("functionCallParams", x => new FsmFunctionCall(x));
        FsmTemplateControlParams = field.GetValueArray("fsmTemplateControlParams", x => new FsmTemplateControl(x));
        FsmEventTargetParams = field.GetValueArray("fsmEventTargetParams", x => new FsmEventTarget(x));
        FsmPropertyParams = field.GetValueArray("fsmPropertyParams", x => new FsmProperty(x));
        LayoutOptionParams = field.GetValueArray("layoutOptionParams", x => new FsmLayoutOption(x));
        FsmStringParams = field.GetValueArray("fsmStringParams", x => new FsmString(x));
        FsmObjectParams = field.GetValueArray("fsmObjectParams", x => new FsmObject(x));
        FsmVarParams = field.GetValueArray("fsmVarParams", x => new FsmVar(x));
        FsmArrayParams = field.GetValueArray("fsmArrayParams", x => new FsmArray(x));
        FsmEnumParams = field.GetValueArray("fsmEnumParams", x => new FsmEnum(x));
        FsmFloatParams = field.GetValueArray("fsmFloatParams", x => new FsmFloat(x));
        FsmIntParams = field.GetValueArray("fsmIntParams", x => new FsmInt(x));
        FsmBoolParams = field.GetValueArray("fsmBoolParams", x => new FsmBool(x));
        FsmVector2Params = field.GetValueArray("fsmVector2Params", x => new FsmVector2(x));
        FsmVector3Params = field.GetValueArray("fsmVector3Params", x => new FsmVector3(x));
        FsmColorParams = field.GetValueArray("fsmColorParams", x => new FsmColor(x));
        FsmRectParams = field.GetValueArray("fsmRectParams", x => new FsmRect(x));
        FsmQuaternionParams = field.GetValueArray("fsmQuaternionParams", x => new FsmQuaternion(x));
        StringParams = field.GetValue<List<string>>("stringParams");
        ByteData = [.. field.GetValue<List<byte>>("byteData")];
        ArrayParamSizes = field.GetValue<List<int>>("arrayParamSizes");
        ArrayParamTypes = field.GetValue<List<string>>("arrayParamTypes");
        CustomTypeSizes = field.GetValue<List<int>>("customTypeSizes");
        CustomTypeNames = field.GetValue<List<string>>("customTypeNames");
        ParamDataType = field.GetValue<List<ParamDataType>>("paramDataType");
        ParamName = field.GetValue<List<string>>("paramName");
        ParamDataPos = field.GetValue<List<int>>("paramDataPos");
        ParamByteDataSize = field.GetValue<List<int>>("paramByteDataSize");
    }
}
