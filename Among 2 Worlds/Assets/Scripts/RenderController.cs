using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderController : MonoBehaviour
{

    Player playerRef;
    Sprite Lilian, Gale;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GetComponent<Player>();
        Lilian = Resources.Load<Sprite>("Lilian");
        Gale = Resources.Load<Sprite>("Gale");
        playerRef.renderRef.sprite = Lilian;
    }

    // Update is called once per frame
    void Update()
    {
        rendersprites();
    }

    void rendersprites()
    {
        switch (playerRef.playerdirection)
        {
            case (Player.direction.Left):
                playerRef.renderRef.flipX = false;
                break;
            case (Player.direction.Right):
                playerRef.renderRef.flipX = true;
                break;
        }

        switch (GameManager.GMInstance.currentdim)
        {
            case (GameManager.dimension.Light):
                playerRef.renderRef.sprite = Lilian;
                break;
            case (GameManager.dimension.Dark):
                playerRef.renderRef.sprite = Gale;
                break;
        }
    }
}
