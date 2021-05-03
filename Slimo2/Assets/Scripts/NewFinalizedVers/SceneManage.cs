using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class SceneManage : MonoBehaviour
{
    public SceneBundles[] scenes;
    // Start is called before the first frame update
    
    public void ChangeLevel(int ID)
    {
        SceneManager.LoadScene(ID);
    }
    public void ChangeLevel(string name)
    {
        foreach(SceneBundles s in scenes)
        {
            if(s.sceneName == name)
            {
                SceneManager.LoadScene(s.sceneID);
            }
        }
    }
    public int ReturnSceneStateType()
    {
        return scenes[SceneManager.GetActiveScene().buildIndex].sceneStateType;
    }
}
[Serializable]
public struct SceneBundles
{
    public string sceneName;
    public int sceneID;
    public int sceneStateType;
}