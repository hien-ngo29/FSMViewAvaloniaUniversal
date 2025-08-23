using FSMExpress.Common.Interfaces;

// todo
namespace FSMExpress.PlayMaker.Structs;
public class FsmVariables
{
    public List<FsmFloat> FloatVariables;
    public List<FsmInt> IntVariables;
    public List<FsmBool> BoolVariables;
    public List<FsmString> StringVariables;
    public List<FsmVector2> Vector2Variables;
    public List<FsmVector3> Vector3Variables;
    public List<FsmColor> ColorVariables;
    public List<FsmRect> RectVariables;
    public List<FsmQuaternion> QuaternionVariables;
    public List<FsmGameObject> GameObjectVariables;
    public List<FsmObject> ObjectVariables;
    public List<FsmMaterial> MaterialVariables;
    public List<FsmTexture> TextureVariables;
    public List<FsmArray> ArrayVariables;
    public List<FsmEnum> EnumVariables;
    public List<string> Categories;
    public List<int> VariableCategoryIds;

    public FsmVariables()
    {
        FloatVariables = [];
        IntVariables = [];
        BoolVariables = [];
        StringVariables = [];
        Vector2Variables = [];
        Vector3Variables = [];
        ColorVariables = [];
        RectVariables = [];
        QuaternionVariables = [];
        GameObjectVariables = [];
        ObjectVariables = [];
        MaterialVariables = [];
        TextureVariables = [];
        ArrayVariables = [];
        EnumVariables = [];
        Categories = [];
        VariableCategoryIds = [];
    }

    public FsmVariables(IAssetField field)
    {
        FloatVariables = field.GetValueArray("floatVariables", x => new FsmFloat(x));
        IntVariables = field.GetValueArray("intVariables", x => new FsmInt(x));
        BoolVariables = field.GetValueArray("boolVariables", x => new FsmBool(x));
        StringVariables = field.GetValueArray("stringVariables", x => new FsmString(x));
        Vector2Variables = field.GetValueArray("vector2Variables", x => new FsmVector2(x));
        Vector3Variables = field.GetValueArray("vector3Variables", x => new FsmVector3(x));
        ColorVariables = field.GetValueArray("colorVariables", x => new FsmColor(x));
        RectVariables = field.GetValueArray("rectVariables", x => new FsmRect(x));
        QuaternionVariables = field.GetValueArray("quaternionVariables", x => new FsmQuaternion(x));
        GameObjectVariables = field.GetValueArray("gameObjectVariables", x => new FsmGameObject(x));
        ObjectVariables = field.GetValueArray("objectVariables", x => new FsmObject(x));
        MaterialVariables = field.GetValueArray("materialVariables", x => new FsmMaterial(x));
        TextureVariables = field.GetValueArray("textureVariables", x => new FsmTexture(x));
        ArrayVariables = field.GetValueArray("arrayVariables", x => new FsmArray(x));
        EnumVariables = field.GetValueArray("enumVariables", x => new FsmEnum(x));
        Categories = field.GetValue<List<string>>("categories");
        VariableCategoryIds = field.GetValue<List<int>>("variableCategoryIDs");
    }
}
