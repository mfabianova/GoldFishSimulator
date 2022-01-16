using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SpatialTracking;
using UnityEngine;

public class Level3PauseScript : MonoBehaviour
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

                    Jelly.GetComponent<Bloom>().active = true;

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
        Jelly.GetComponent<Bloom>().active = true;

        isPause = false;
        canvas.SetActive(false);
        Time.timeScale = 1;

        GetComponent<TrackedPoseDriver>().enabled = true;
    }

    private void PauseGame()
    {
        Jelly.GetComponent<Bloom>().active = false;

        isPause = true;
        canvas.SetActive(true);
        Time.timeScale = 0;

        GetComponent<TrackedPoseDriver>().enabled = false;
    }
}
