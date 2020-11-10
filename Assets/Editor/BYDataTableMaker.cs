
using UnityEngine;
using UnityEditor;
using System.IO;

public class BYDataTableMaker : MonoBehaviour
{
    [MenuItem("Assets/BY/CreateBinaryDataFromCSV",false,1)]
    public static void CreateBinaryDataFromCSV()
    {
        foreach (UnityEngine.Object e in Selection.objects)
        {
            TextAsset csvFile = (TextAsset)e;
            string nameTable = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(csvFile));

            ScriptableObject scriptableObject = ScriptableObject.CreateInstance(nameTable);
            if (scriptableObject != null)
            {
                AssetDatabase.CreateAsset(scriptableObject, "Assets/Resources/DataTable/" + nameTable + ".asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                BYDataTableCreate bycreate = (BYDataTableCreate)scriptableObject;
                bycreate.ImportData(csvFile);
                EditorUtility.SetDirty(bycreate);


            }
        }

    }
}
