using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;

public class EditorMods : Editor
{
    [MenuItem("My Tools/Scenes/menu")]
    static void menuScene()
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Scenes/menu.unity");
        }
    }
    [MenuItem("My Tools/Scenes/main")]
    static void mainScene()
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Scenes/main.unity");
        }
    }
}
