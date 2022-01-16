using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.SpatialTracking;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class LevelPauseScript : MonoBehaviour
{
    private bool tapping;   //ci cakama na 2. klik
    private bool isPause;
    private float lastTapTime = 0;  //cas posledneho kliku
    private float TimeTolerance = .25f;  //max cas medzi 2 klikmi aby bol dovjklik

    public GameObject canvas;
    public GameObject Jelly;

    // Start is called before the first frame update
    void Start()
    {
        tapping = false;
        isPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPause)
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)   //ak obrazovka detekuje dotyk
            {
                if (!tapping)    //ak necakam na 2. klik tj je to 1. klik alebo ostamoteny
                {
                    tapping = true;
                    StartCoroutine(SingleTap());
                }

                if ((Time.unscaledTime - lastTapTime) < TimeTolerance)
                {
                    //nastal doubletap
                    if (Jelly != null)
                    {
                        Jelly.active = true;
                    }
                    Time.timeScale = 1;
                    SceneManager.LoadScene("menu", LoadSceneMode.Single);

                    tapping = false;
                }
                lastTapTime = Time.unscaledTime;
            }
        }
        else
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)   //ak obrazovka detekuje dotyk
            {
                PauseGame();
                lastTapTime = Time.unscaledTime;
            }   
        }
                
    }

    public IEnumerator SingleTap()
    {
        yield return new WaitForSecondsRealtime(TimeTolerance); //cakam ci nenastane druhy tuk a nezmeni taping na false
        if (tapping)
        {
            
            ReturnToGame();

            tapping = false;
           
        }
    }

    private void ReturnToGame()
    {

        if(Jelly != null)
        {
            Jelly.active = true;
        }

        isPause = false;
        canvas.SetActive(false);
        

        GetComponent<TrackedPoseDriver>().enabled = true;

        Time.timeScale = 1;
    }

    private void PauseGame()
    {
        Time.timeScale = 0;

        isPause = true;
        canvas.SetActive(true);
        

        GetComponent<TrackedPoseDriver>().enabled = false;

        if (Jelly != null)
        {
            Jelly.active = false;
        }
    }
}

