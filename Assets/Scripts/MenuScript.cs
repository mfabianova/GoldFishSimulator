using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public float a = 0;
    
    public void LoadLevel(string scene)
    {
        SceneManager.LoadScene(scene,LoadSceneMode.Single);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("menu", LoadSceneMode.Single)
;    }
}
