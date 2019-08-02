using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    Player playerRef;
    private Animator animator;

    //Awake is called before Start.
    private void Awake()
    {
        playerRef = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {

        float characterSpeed = Mathf.Abs(playerRef.rigidRef.velocity.x);
        animator.SetFloat("Speed", characterSpeed);
    
    }
}
