using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSFXManager : MonoBehaviour
{
    [SerializeField] Knight knightRef;
    [SerializeField] SFXManager sfxRef;
    // Start is called before the first frame update
    void Start()
    {
        knightRef = GetComponentInParent<Knight>();
        sfxRef = FindObjectOfType<SFXManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CallKnightHitSound();
    }

    public void CallKnightHitSound()
    {
        if (knightRef.knightState == Knight.enemyState.Attacking)
        {
            sfxRef.playSound(sfxRef.current_audioclip, sfxRef.KnightGettingHit);
            Debug.Log(knightRef.knightState);
        }
    }
}
