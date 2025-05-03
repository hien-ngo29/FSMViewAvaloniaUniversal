using AssetsTools.NET.Cpp2IL;
using AssetsTools.NET.Extra;
using System;
using System.IO;

namespace FSMExpress.Logic.Util;
public static class AssetsManagerExtensions
{
    public static bool LoadClassDatabase(this AssetsManager manager, AssetsFileInstance forFile)
    {
        if (manager.ClassPackage == null)
        {
            string classDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "classdata.tpk");
            if (File.Exists(classDataPath))
                manager.LoadClassPackage(classDataPath);
            else
                return false;
        }

        if (manager.ClassDatabase == null)
        {
            manager.LoadClassDatabaseFromPackage(forFile.file.Metadata.UnityVersion);
        }

        return true;
    }

    public static bool LoadMonoBehaviours(this AssetsManager manager, AssetsFileInstance forFile)
    {
        if (manager.MonoTempGenerator is null)
        {
            var fileDir = PathUtils.GetAssetsFileDirectory(forFile);
            var managedDir = Path.Combine(fileDir, "Managed");
            if (Directory.Exists(managedDir))
            {
                bool hasDll = Directory.GetFiles(managedDir, "*.dll").Length > 0;
                if (hasDll)
                {
                    manager.MonoTempGenerator = new MonoCecilTempGenerator(managedDir);
                    return true;
                }
            }

            var il2cppFiles = FindCpp2IlFiles.Find(fileDir);
            if (il2cppFiles.success)
            {
                manager.MonoTempGenerator = new Cpp2IlTempGenerator(il2cppFiles.metaPath, il2cppFiles.asmPath);
                return true;
            }

            return false;
        }

        return true;
    }
}
