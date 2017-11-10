using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuItem_AddFolder : EditorWindow
{
    //editorWindow!
    System.Collections.Generic.List<string> folderNames = new System.Collections.Generic.List<string>();
    System.Collections.Generic.List<string> subFolderNames = new System.Collections.Generic.List<string>();
    System.Collections.Generic.List<bool> hasSubfolder = new System.Collections.Generic.List<bool>();
    System.Collections.Generic.List<int> subfolderCount = new System.Collections.Generic.List<int>();
    int subfolderCountSlider = 1;


    #region menuItem
    [MenuItem("MyTools/Folder Maker &#f")]
    static void CreateNewFolder()
    {
        MenuItem_AddFolder window = (MenuItem_AddFolder)GetWindow(typeof(MenuItem_AddFolder));
        window.Show();
    }
    #endregion

    void OnGUI()
    {
        GUILayout.Label("Folder Maker", EditorStyles.boldLabel);
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        GUILayout.Label(path, "Box");
        EditorGUILayout.IntField("Number of folders to add: ", folderNames.Count);
        #region Adjust numberOfFolders Count

        GUILayout.BeginHorizontal("Box");
        if (GUILayout.Button("Increase with 1"))
        {
            folderNames.Add("");
            hasSubfolder.Add(false);
            subfolderCount.Add(0);
        }
        GUILayout.Space(5);
        if (GUILayout.Button("Decrease with 1") && folderNames.Count > 0)
        {
            folderNames.RemoveAt(folderNames.Count - 1);
            hasSubfolder.RemoveAt(hasSubfolder.Count - 1);
            subfolderCount.RemoveAt(subfolderCount.Count - 1);
        }
        GUILayout.EndHorizontal();
        #endregion

        GUILayout.Space(10);

        for (int i = 0; i < folderNames.Count; i++)
        {
            GUILayout.BeginHorizontal("Box");
            folderNames[i] = EditorGUILayout.TextField("Name of folder: ", folderNames[i]);
            hasSubfolder[i] = GUILayout.Toggle(hasSubfolder[i], "SubFolder");
            if (hasSubfolder[i]) // toggle = true
            {
                subfolderCount[i] = EditorGUILayout.IntSlider(subfolderCount[i], 1, 5, GUILayout.MaxWidth(125f)); 
            }
            GUILayout.EndHorizontal();

            if (hasSubfolder[i] && subfolderCount[i] >= 0) // hier worden subfolders geshowed
            {   // ze worden atm als een batch gemaakt met dezelfde namen, moeten dus nog apparte strings krijgen - de subfolders door laten counten in de int list is een idee
                for (int it = 0; it < subfolderCount[i]; it++)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(30);
                    subFolderNames.Add(""); 
                    subFolderNames[it] = EditorGUILayout.TextField("Subfolder Name", subFolderNames[it]);
                    GUILayout.EndHorizontal();
                }
            }
        }

        GUILayout.Space(25);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Sort", GUILayout.Width(100f)))
        {   //subfolder sort komt er nog bij nadat die ook multiple kunnen adden
            folderNames.Sort();
        }

        GUILayout.Space(30);

        if (GUILayout.Button("Create", GUILayout.Width(100f)))
        {
            for (int i = 0; i < folderNames.Count; i++)
            {
                string guid = AssetDatabase.CreateFolder(path, folderNames[i]);
                string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);
                if (hasSubfolder[i] == true)
                {
                    string subPath = (path + "/" + folderNames[i]);
                    string subGuid = AssetDatabase.CreateFolder(subPath, subFolderNames[i]);
                    string subFolderPath = AssetDatabase.GUIDToAssetPath(subGuid);
                }
            }
            //EditorApplication.Beep();
        }

        GUILayout.Space(100);

        GUI.color = new Color(1,0,0,.75f);
        if (GUILayout.Button("Close" ,GUILayout.Width(50f), GUILayout.Height(25f)))
        {
            this.Close();
        }
        GUILayout.EndHorizontal();
    }
    
    /* Ideeén
        Sorting
    */
}
// check voor als file path exists en wilt replacen
//else if (Path.GetExtension(path) != "")
//{
//    path = path.Replace(Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");
//}
//AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath (path + "/New MyAsset.asset"));


//[MenuItem("GameObject/Create Folder")]
//static void CreateMaterial()
//{
//    string guid = AssetDatabase.CreateFolder("_JustScripts", "My Folder");
//    string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);
//}
//}
