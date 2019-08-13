using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Sprite LightSprite, DarkSprite;
    SpriteRenderer renderRef;

    private void Awake()
    {
        renderRef = GetComponent<SpriteRenderer>();
        updateDimensions();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateDimensions()
    {
        switch (GameManager.GMInstance.currentdim)
        {
            case (GameManager.dimension.Light):
                renderRef.sprite = LightSprite;
                break;
            case (GameManager.dimension.Dark):
                renderRef.sprite = DarkSprite;
                break;
        }
    }

}
