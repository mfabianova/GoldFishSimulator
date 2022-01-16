using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuScriptTap : MonoBehaviour
{
    private bool tapping;   //ci cakama na 2. klik
    private float lastTapTime = 0;  //cas posledneho kliku
    private float TimeTolerance = .25f;  //max cas medzi 2 klikmi aby bol dovjklik
    private Color colSelected = new Color(0.0f, 0.7020f, 0.4431f, 1.0f); 
    private Color colDeselected = new Color(0.6314f, 0.7804f, 0.7255f, 1.0f);
    public Image EasyRect, MediumRect, HardRect;
    private string selected_scene_name = "Level1";


    // Start is called before the first frame update
    void Start()
    {
        tapping = false;    
        EasyRect.color = colSelected;
        MediumRect.color = colDeselected;
        HardRect.color = colDeselected;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)   //ak obrazovka detekuje dotyk
        {

            if (!tapping)    //ak necakam na 2. klik tj je to 1. klik alebo ostamoteny
            {
                tapping = true;
                StartCoroutine(SingleTap());
            }

            if (( Time.unscaledTime - lastTapTime) < TimeTolerance)
            {
                //nastal doubletap
                LoadLevel(selected_scene_name);
                tapping = false;
            }
            lastTapTime = Time.unscaledTime;
        }
        
    }

    public void LoadLevel(string scene)
    {
        if(scene == "Level1")
        {
            EasyRect.GetComponent<Button>().onClick.Invoke();
        }
        else if (scene == "Level2")
        {
            MediumRect.GetComponent<Button>().onClick.Invoke();
        }
        else if (scene == "Level3")
        {
            HardRect.GetComponent<Button>().onClick.Invoke();
        }

    }

    public IEnumerator SingleTap()
    {
        yield return new WaitForSeconds(TimeTolerance); //cakam ci nenastane druhy tuk a nezmeni taping na false
        if (tapping)
        {
            //posuniem vyber scneny
            if (selected_scene_name == "Level1")
            {
                selected_scene_name = "Level2";
                EasyRect.color = colDeselected;
                MediumRect.color = colSelected;
            }
            else if (selected_scene_name == "Level2")
            {
                selected_scene_name = "Level3";
                MediumRect.color = colDeselected;
                HardRect.color = colSelected;
            }
            else if (selected_scene_name == "Level3")
            {
                selected_scene_name = "Level1";
                HardRect.color = colDeselected;
                EasyRect.color = colSelected;
            }
            

            tapping = false;
        }
    }
}
