using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSequence : MonoBehaviour
{
    [SerializeField]
    Player playerRef;
    [SerializeField]
    Rigidbody2D rigidRef;
    public bool DeathSequenceIsPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        playerRef = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayDeathSequence();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DeathSequenceIsPlaying = true;
            Debug.Log("player ded boi");
            RespawnPlayer();
        }
    }

    void PlayDeathSequence()
    {
        if (Input.GetKey(KeyCode.F) && DeathSequenceIsPlaying == false)
        {
            DeathSequenceIsPlaying = true;
            Debug.Log("player ded boi");
            RespawnPlayer();
        }
    }

    void RespawnPlayer()
    {
        playerRef.transform.position = new Vector2(0.299f, 2f);
        DeathSequenceIsPlaying = false;
        playerRef.rigidRef.velocity = new Vector2(0, 0);
    }
}
