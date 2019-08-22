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
        bool LightDimension;
        if(GameManager.GMInstance.currentdim == GameManager.dimension.Light)
        {
            LightDimension = true;
        }
        else
        {
            LightDimension = false;
        }

        animator.SetBool("LightDimension", LightDimension);
    }
}
