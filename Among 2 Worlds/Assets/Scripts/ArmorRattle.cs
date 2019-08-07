using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorRattle : MonoBehaviour
{
    [SerializeField]
    AudioSource current_audioclip;
    public AudioClip rattle;

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
            playRattleSound();
            Debug.Log("rattle sound played");
        }

        if ((Input.GetKeyUp(KeyCode.A) && !Input.GetKey(KeyCode.D)) || (Input.GetKeyUp(KeyCode.D) && !Input.GetKey(KeyCode.D)))
        {
            stopRattleSound();
            Debug.Log("rattle sound stopped");
        }
    }

    public void playRattleSound()
    {
        current_audioclip.clip = rattle;
        current_audioclip.Play();
    }

    public void stopRattleSound()
    {
        current_audioclip.clip = rattle;
        current_audioclip.Stop();
    }
}
