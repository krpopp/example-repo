using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{

    //public so that the button can access it
    public void LoadGame(string sceneToLoad)
    {
        //use the scene manager to load the game scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
