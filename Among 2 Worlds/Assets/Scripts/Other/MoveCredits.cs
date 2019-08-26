using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCredits : MonoBehaviour
{
    public GameObject credits;
    [SerializeField] Rigidbody2D rigRef;

    private void Start()
    {
        rigRef = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        rigRef.velocity = new Vector2(0, 1.3f);
    }
}
