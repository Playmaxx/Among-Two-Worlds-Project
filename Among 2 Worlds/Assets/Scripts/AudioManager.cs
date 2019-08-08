using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource current_track;
    public AudioClip CuteGiana;
    public AudioClip DarkGiana;
    public float timecode1;
    public float timecode2;

    // Start is called before the first frame update
    void Start()
    {
        current_track = GetComponent<AudioSource>();
        current_track.volume = 1.0f;
        current_track.clip = CuteGiana;
        current_track.Play();
        
    }
    // Update is called once per frame
    void Update()
    {
        timecode1 = timecode2;
        timecode2 = current_track.time;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (current_track.clip == CuteGiana)
            {
                current_track.Stop();
                current_track.clip = DarkGiana;
                current_track.time = timecode1;
                current_track.Play();
            }

            else
            {
                current_track.Stop();
                current_track.clip = CuteGiana;
                current_track.time = timecode1;
                current_track.Play();
            }

            Debug.Log(current_track.time);
            Debug.Log(current_track.clip);
        }
    }
}
