using AssetsTools.NET.Extra;
using System.IO;

namespace FSMExpress.Logic.Util;
public class PathUtils
{
    public static string GetAssetsFileDirectory(AssetsFileInstance fileInst)
    {
        if (fileInst.parentBundle != null)
        {
            string dir = Path.GetDirectoryName(fileInst.parentBundle.path)!;

            // addressables
            string? upDir = Path.GetDirectoryName(dir);
            string? upDir2 = Path.GetDirectoryName(upDir ?? string.Empty);
            if (upDir != null && upDir2 != null)
            {
                if (Path.GetFileName(upDir) == "aa" && Path.GetFileName(upDir2) == "StreamingAssets")
                {
                    dir = Path.GetDirectoryName(upDir2)!;
                }
            }

            return dir;
        }
        else
        {
            string dir = Path.GetDirectoryName(fileInst.path)!;
            if (fileInst.name == "unity default resources" || fileInst.name == "unity_builtin_extra")
            {
                dir = Path.GetDirectoryName(dir)!;
            }

            return dir;
        }
    }
}
