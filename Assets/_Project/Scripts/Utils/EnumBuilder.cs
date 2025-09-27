#if UNITY_EDITOR
using UnityEditor;
using System.IO;

namespace FunnyBlox.Utils
{
public class EnumBuilder
{
    public static void Build(string enumName, string[] enumEntries)
    {
        string filePathAndName = "Assets/_Project/Sources/Data/Enums/" + enumName + ".cs"; //The folder Scripts/Enums/ is expected to exist

        using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
        {
            streamWriter.WriteLine("public enum " + enumName);
            streamWriter.WriteLine("{");
            for (int i = 0; i < enumEntries.Length; i++)
            {
                streamWriter.WriteLine("\t" + enumEntries[i] + ",");
            }
            streamWriter.WriteLine("}");
        }
        AssetDatabase.Refresh();
    }
}
}
#endif
