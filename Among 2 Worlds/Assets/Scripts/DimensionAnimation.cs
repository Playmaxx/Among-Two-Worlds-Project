using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.GMInstance.currentdim == GameManager.dimension.Light)
        {
            animator.SetBool("LightDimension", true);
        }
        else
        {
            animator.SetBool("LightDimension", false);
        }

    }
}
