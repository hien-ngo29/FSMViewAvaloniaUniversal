using AssetsTools.NET;
using FSMExpress.Common.Interfaces;

namespace FSMExpress.Common.Assets;
public class AfAssetField(AssetTypeValueField valueField, AfAssetNamer namer) : IAssetField
{
    public bool Exists(string name)
    {
        return valueField[name].IsDummy;
    }

    public bool Exists(int index)
    {
        return valueField[index].IsDummy;
    }

    public IAssetField GetField(string name)
    {
        return new AfAssetField(valueField[name], namer);
    }

    public IAssetField GetField(int index)
    {
        return new AfAssetField(valueField[index], namer);
    }

    public T GetValue<T>(string name)
    {
        return GetValueImpl<T>(valueField[name]);
    }

    public T GetValue<T>(int index)
    {
        return GetValueImpl<T>(valueField[index]);
    }

    public List<T> GetValueArray<T>(string name, Func<IAssetField, T> mapper)
    {
        return GetValueArrayImpl(valueField[name]["Array"], mapper);
    }

    public List<T> GetValueArray<T>(int index, Func<IAssetField, T> mapper)
    {
        return GetValueArrayImpl(valueField[index][0], mapper);
    }

    private T GetValueImpl<T>(AssetTypeValueField field)
    {
        Type paramType = typeof(T);
        Type genericType;

        if (paramType == typeof(int) || paramType.IsEnum)
        {
            return (T)(object)field.AsInt;
        }
        else if (paramType == typeof(float))
        {
            return (T)(object)field.AsFloat;
        }
        else if (paramType == typeof(NamedAssetPPtr))
        {
            var fileId = field["m_FileID"].AsInt;
            var pathId = field["m_PathID"].AsLong;
            var name = namer.GetName(fileId, pathId) ?? string.Empty;

            var pptr = new NamedAssetPPtr(string.Empty, fileId, pathId, name);
            namer.NameAssetPPtrFile(pptr);

            return (T)(object)pptr;
        }
        else if (paramType.IsGenericType && (genericType = paramType.GetGenericTypeDefinition()) == typeof(List<>))
        {
            return GetValueArrayPrimitiveImpl<T>(field["Array"], paramType.GetGenericArguments()[0]);
        }
        else if (paramType == typeof(bool))
        {
            return (T)(object)(field.AsByte != 0);
        }
        else if (paramType == typeof(long))
        {
            return (T)(object)field.AsLong;
        }
        else if (paramType == typeof(string))
        {
            return (T)(object)field.AsString;
        }
        else if (paramType == typeof(byte))
        {
            return (T)(object)field.AsByte;
        }
        else if (paramType == typeof(uint))
        {
            return (T)(object)field.AsUInt;
        }
        else if (paramType == typeof(ushort))
        {
            return (T)(object)field.AsUShort;
        }
        else if (paramType == typeof(sbyte))
        {
            return (T)(object)field.AsSByte;
        }
        else if (paramType == typeof(short))
        {
            return (T)(object)field.AsShort;
        }
        else if (paramType == typeof(ulong))
        {
            return (T)(object)field.AsULong;
        }
        else if (paramType == typeof(double))
        {
            return (T)(object)field.AsDouble;
        }

        throw new ArgumentException($"Not a valid type to read for field ({paramType.Name})");
    }

    private T GetValueArrayPrimitiveImpl<T>(AssetTypeValueField field, Type genericType)
    {
        if (genericType == typeof(int))
        {
            var list = new List<int>(field.Children.Count);
            foreach (var child in field)
                list.Add(child.AsInt);

            return (T)(object)list;
        }
        else if (genericType.IsEnum)
        {
            var list = new List<int>(field.Children.Count);
            foreach (var child in field)
                list.Add(child.AsInt);

            var method = typeof(Enumerable).GetMethod("Cast")?.MakeGenericMethod(genericType)
                ?? throw new Exception("couldn't create enum list");

            var enumList = list.Select(i => Enum.ToObject(genericType, i));
            var casted = method.Invoke(null, [enumList]);
            var newList = typeof(Enumerable).GetMethod("ToList")?.MakeGenericMethod(genericType).Invoke(null, [casted])
                ?? throw new Exception("couldn't create enum list");

            return (T)newList;
        }
        else if (genericType == typeof(float))
        {
            var list = new List<float>(field.Children.Count);
            foreach (var child in field)
                list.Add(child.AsFloat);

            return (T)(object)list;
        }
        else if (genericType == typeof(NamedAssetPPtr))
        {
            var list = new List<NamedAssetPPtr>(field.Children.Count);
            foreach (var child in field)
            {
                var fileId = child["m_FileID"].AsInt;
                var pathId = child["m_PathID"].AsLong;
                var name = namer.GetName(fileId, pathId) ?? string.Empty;

                var pptr = new NamedAssetPPtr(string.Empty, fileId, pathId, name);
                namer.NameAssetPPtrFile(pptr);

                list.Add(pptr);
            }

            return (T)(object)list;
        }
        else if (genericType == typeof(bool))
        {
            var list = new List<bool>(field.Children.Count);
            foreach (var child in field)
                list.Add(child.AsByte != 0);

            return (T)(object)list;
        }
        else if (genericType == typeof(long))
        {
            var list = new List<long>(field.Children.Count);
            foreach (var child in field)
                list.Add(child.AsLong);

            return (T)(object)list;
        }
        else if (genericType == typeof(string))
        {
            var list = new List<string>(field.Children.Count);
            foreach (var child in field)
                list.Add(child.AsString);

            return (T)(object)list;
        }
        else if (genericType == typeof(byte))
        {
            // https://github.com/nesrak1/AssetsTools.NET/blob/a73a399cee3fedadcfa3c1861e6192414ad588fb/AssetTools.NET/Extra/MonoDeserializer/CommonMonoTemplateHelper.cs#L213
            // it appears mono deserializers automatically turn arrays of bytes
            // into ByteArray typed fields, so we have to use .AsByteArray here.
            return (T)(object)field.AsByteArray.ToList();
        }
        else if (genericType == typeof(uint))
        {
            var list = new List<uint>(field.Children.Count);
            foreach (var child in field)
                list.Add(child.AsUInt);

            return (T)(object)list;
        }
        else if (genericType == typeof(ushort))
        {
            var list = new List<ushort>(field.Children.Count);
            foreach (var child in field)
                list.Add(child.AsUShort);

            return (T)(object)list;
        }
        else if (genericType == typeof(sbyte))
        {
            var list = new List<sbyte>(field.Children.Count);
            foreach (var child in field)
                list.Add(child.AsSByte);

            return (T)(object)list;
        }
        else if (genericType == typeof(short))
        {
            var list = new List<short>(field.Children.Count);
            foreach (var child in field)
                list.Add(child.AsShort);

            return (T)(object)list;
        }
        else if (genericType == typeof(ulong))
        {
            var list = new List<ulong>(field.Children.Count);
            foreach (var child in field)
                list.Add(child.AsULong);

            return (T)(object)list;
        }
        else if (genericType == typeof(double))
        {
            var list = new List<double>(field.Children.Count);
            foreach (var child in field)
                list.Add(child.AsDouble);

            return (T)(object)list;
        }

        throw new ArgumentException($"Not a valid type to read for list field ({genericType.Name})");
    }

    private List<T> GetValueArrayImpl<T>(AssetTypeValueField field, Func<IAssetField, T> mapper)
    {
        var list = new List<T>(field.Children.Count);
        foreach (var child in field)
            list.Add(mapper(new AfAssetField(child, namer)));

        return list;
    }
}
