using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameManager.dimension ownDimension;
    public enum MyColliderType { Box, Polygon }
    SpriteRenderer renderRef;

    public MyColliderType ColliderType;
    public Sprite LightSprite;
    public Sprite DarkSprite;

    //Awake is called before Start
    private void Awake()
    {
        renderRef = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        updateDimensions();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void updateDimensions()
    {
        if (ownDimension == GameManager.GMInstance.currentdim || ownDimension == GameManager.dimension.None)
        {
            switch (ColliderType)
            {
                case (MyColliderType.Box):
                    GetComponent<BoxCollider2D>().enabled = true;
                    break;
                case (MyColliderType.Polygon):
                    GetComponent<PolygonCollider2D>().enabled = true;
                    break;
            }
            GetComponent<SpriteRenderer>().enabled = true;
            switch (GameManager.GMInstance.currentdim)
            {
                case (GameManager.dimension.Light):
                    renderRef.sprite = LightSprite;
                    break;
                case (GameManager.dimension.Dark):
                    renderRef.sprite = DarkSprite;
                    break;
            }
            gameObject.layer = 10;
        }
        else
        {
            switch (ColliderType)
            {
                case (MyColliderType.Box):
                    GetComponent<BoxCollider2D>().enabled = false;
                    break;
                case (MyColliderType.Polygon):
                    GetComponent<PolygonCollider2D>().enabled = false;
                    break;

            }
            GetComponent<SpriteRenderer>().enabled = false;
            gameObject.layer = 0;
        }
    }
}