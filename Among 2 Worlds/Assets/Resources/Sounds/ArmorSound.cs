using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorSound : MonoBehaviour
{
    [SerializeField]
    AudioSource current_audioclip;
    public AudioClip ArmorSoundWhileRunning;

    private void Awake()
    {
        current_audioclip = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
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
            playArmorSound();
            Debug.Log("armor sound played");
        }

        if ((Input.GetKeyUp(KeyCode.A) && !Input.GetKey(KeyCode.D)) || (Input.GetKeyUp(KeyCode.D) && !Input.GetKey(KeyCode.D)))
        {
            stopArmorSound();
            Debug.Log("armor sound stopped");
        }
    }

    public void playArmorSound()
    {
        current_audioclip.clip = ArmorSoundWhileRunning;
        current_audioclip.Play();
    }

    public void stopArmorSound()
    {
        current_audioclip.clip = ArmorSoundWhileRunning;
        current_audioclip.Stop();
    }
}
