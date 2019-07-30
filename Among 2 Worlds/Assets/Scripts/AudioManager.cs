using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource GTD;
    public AudioClip CuteGiana;
    public AudioClip DarkGiana;
    public float timecode1;
    public float timecode2;

    // Start is called before the first frame update
    void Start()
    {
        GTD = GetComponent<AudioSource>();
        GTD.volume = 1.0f;
        GTD.clip = CuteGiana;
        GTD.Play();
        
    }
    // Update is called once per frame
    void Update()
    {
        timecode1 = timecode2;
        timecode2 = GTD.time;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GTD.clip == CuteGiana)
            {
                GTD.Stop();
                GTD.clip = DarkGiana;
                GTD.time = timecode1;
                GTD.Play();
            }

            else
            {
                GTD.Stop();
                GTD.clip = CuteGiana;
                GTD.time = timecode1;
                GTD.Play();
            }

            Debug.Log(GTD.time);
            //Debug.Log(timecode);

        }
    }
}
