using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
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
        //current_track.volume = current_track.volume - 0.02f;
        timecode1 = timecode2;
        timecode2 = current_track.time;
        if (Input.GetKeyDown(KeyCode.E))
        {
            current_track.volume = current_track.volume - 0.02f;
            StartCoroutine(SilenceMusic(0.1f));
            //StartCoroutine(SwitchMusicAfterTime(3f));
        }

        Debug.Log(current_track.clip);
    }

    IEnumerator SwitchMusicAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (current_track.clip == CuteGiana)
        {
            current_track.Stop();
            current_track.clip = DarkGiana;
            current_track.volume = 0.0f;
            current_track.time = timecode1;
            current_track.Play();
            Debug.Log("CuteGiana switched to DarkGiana");
            current_track.volume = current_track.volume + 0.02f;
        }

        //if (current_track.clip == DarkGiana)
        //{
            //current_track.Stop();
            //current_track.clip = CuteGiana;
            //current_track.volume = 0.3f;
            //current_track.time = timecode1;
            //current_track.Play();
            //Debug.Log("DarkGiana switched do CuteGiana");
            //current_track.volume = current_track.volume + 0.02f;
        //}
    }

    IEnumerator SilenceMusic(float time)
    {
        yield return new WaitForSeconds(time);
        while (current_track.volume < 1.0f)
        {
            current_track.volume = current_track.volume - 0.002f;
            Debug.Log("vol not 0 yet");
        }
    }
}
