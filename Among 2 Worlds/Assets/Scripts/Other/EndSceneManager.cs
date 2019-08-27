using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    public CanvasGroup Current_CG;
    int TargetTransparency;

    // Start is called before the first frame update
    void Start()
    {
        Current_CG = GameObject.Find("SiegBild").GetComponent<CanvasGroup>();
        Current_CG.alpha = 0;
        TargetTransparency = 1;
        Debug.Log(TargetTransparency);
    }

    // Update is called once per frame
    void Update()
    {
        adjustFadeState();
        adjustTarget();
        //Debug.Log(Current_CG.alpha);
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(2);
        Current_CG.alpha += 0.5f * Time.deltaTime;
    }

    void FadeOut()
    {
        //Current_CG.alpha -= 0.5f * Time.deltaTime;
        StartCoroutine(loadCredits());
        Debug.Log("fadeout");
    }

    IEnumerator loadCredits()
    {
        yield return new WaitForSeconds(3);
        Destroy(GameObject.Find("AudioManager"));
        SceneManager.LoadScene("Credits");
    }

    void adjustTarget()
    {
        if (Input.anyKeyDown && TargetTransparency == 1)
        {
            TargetTransparency = 0;
            Debug.Log(TargetTransparency);
        }

        else
        {
            TargetTransparency = 1;
        }
    }

    void adjustFadeState()
    {
        if (TargetTransparency == 1)// && x.alpha < 1)
        {
            StartCoroutine(FadeIn());
        }

        if (TargetTransparency == 0)// && x.alpha > 0)
        {
            FadeOut();
        }
    }
}
