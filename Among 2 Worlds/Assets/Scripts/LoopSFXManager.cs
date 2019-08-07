using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopSFXManager : MonoBehaviour
{
    [SerializeField]
    AudioSource current_audioclip;
    public AudioClip Step;
    public AudioClip ArmorSoundWhileRunning;
    // Start is called before the first frame update
    void Awake()
    {
        current_audioclip = GetComponent<AudioSource>();
    }
    void Start()
    {
        current_audioclip.volume = 1.0f;
        current_audioclip.clip = null;
    }

    // Update is called once per frame
    void Update() //Inhalt ist nur für mich zum testen und kann gelöscht werden
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            playStepSound();
        }

        if ((Input.GetKeyUp(KeyCode.A) && !Input.GetKey(KeyCode.D)) || (Input.GetKeyUp(KeyCode.D) && !Input.GetKey(KeyCode.D)))
        {
            stopStepSound();
        }
    }

    public void playStepSound()
    {
        current_audioclip.clip = Step;
        current_audioclip.Play();
    }

    public void stopStepSound()
    {
        current_audioclip.clip = Step;
        current_audioclip.Stop();
    }
}
