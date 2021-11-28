using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public float a = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevel(string scene)
    {
        SceneManager.LoadScene(scene,LoadSceneMode.Single);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("menu", LoadSceneMode.Single)
;    }
}
