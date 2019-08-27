using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour
{
    public CanvasGroup Current_CG;
    public CanvasGroup Bild1_CG;
    public CanvasGroup Bild2_CG;
    public CanvasGroup Bild3_CG;
    public CanvasGroup Bild4_CG;
    int TargetTransparency = 0;
    int Counter = 0;

    bool CG1_Done = false;
    bool CG2_Done = false;
    bool CG3_Done = false;
    bool CG4_Done = false;

    // Start is called before the first frame update
    void Start()
    {
        Current_CG = GameObject.Find("Bild 1").GetComponent<CanvasGroup>();
        InitializeInstances();
        SetAlphaTo0();
        TargetTransparency = 1;
    }

    // Update is called once per frame
    void Update()
    {
        IncrementCounter();
        adjustFadeState(Current_CG);
        CallLoadPic();
        Debug.Log(Counter);
    }

    void InitializeInstances()
    {
        Bild1_CG = GameObject.Find("Bild 1").GetComponent<CanvasGroup>();
        Bild2_CG = GameObject.Find("Bild 2").GetComponent<CanvasGroup>();
        Bild3_CG = GameObject.Find("Bild 3").GetComponent<CanvasGroup>();
        Bild4_CG = GameObject.Find("Bild 4").GetComponent<CanvasGroup>();
    }

    void SetAlphaTo0()
    {
        Bild1_CG.alpha = 0;
        Bild2_CG.alpha = 0;
        Bild3_CG.alpha = 0;
        Bild4_CG.alpha = 0;
    }

    void IncrementCounter()
    {
        if (Input.anyKeyDown)
        {
            Counter++;
        }
    }

    void SetTargetTransparency(int x)
    {
        TargetTransparency = x;
    }

    void FadeIn(CanvasGroup x)
    {
        //Debug.Log(Time.deltaTime);
        x.alpha += 0.5f * Time.deltaTime;
    }

    void FadeOut(CanvasGroup x)
    {
        x.alpha -= 0.5f * Time.deltaTime;
    }

    void LoadPic(CanvasGroup previousCG, CanvasGroup nextCG)
    {
        TargetTransparency = 0;
        adjustFadeState(previousCG);

        if (previousCG.alpha <= 0)
        {
            Current_CG = nextCG;
            TargetTransparency = 1;
        }
    }

    void CallLoadPic()
    {
        switch (Counter)
        {
            case (1):
                LoadPic(Bild1_CG, Bild2_CG);
                break;

            case (2):
                LoadPic(Bild2_CG, Bild3_CG);
                break;

            case (3):
                LoadPic(Bild3_CG, Bild4_CG);
                break;

            case (4):
                SceneManager.LoadScene("Level 1");
                break;
        }
    }

    void adjustFadeState(CanvasGroup x)
    {
        if (TargetTransparency == 1)// && x.alpha < 1)
        {
            FadeIn(x);
        }

        if (TargetTransparency == 0)// && x.alpha > 0)
        {
            FadeOut(x);
        }
    }
}
