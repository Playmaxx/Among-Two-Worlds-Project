using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLeverAnimationController : MonoBehaviour
{

    public Animator animator;

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isOn;
        if (GameManager.GMInstance.DungeonLever == true)
        {
            isOn = true;
        }
        else
        {
            isOn = false;
        }
        animator.SetBool("isOn", isOn);
    }

}