namespace FSMExpress.Common.Interfaces;
public interface IAssetField
{
    bool Exists(string name);
    bool Exists(int index);

    T GetValue<T>(string name);
    T GetValue<T>(int index);

    List<T> GetValueArray<T>(string name, Func<IAssetField, T> mapper);
    List<T> GetValueArray<T>(int index, Func<IAssetField, T> mapper);

    IAssetField GetField(string name);
    IAssetField GetField(int index);
}
