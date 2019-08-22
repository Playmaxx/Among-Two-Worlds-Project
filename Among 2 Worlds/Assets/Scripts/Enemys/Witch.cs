using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : MonoBehaviour
{
    [SerializeField] private float Speed = 1f;
    [SerializeField] private int DamageGiven = 50;
    [SerializeField] private int DamageCooldown = 60;
    private int AttackCooldown = 0;
    //  [SerializeField] private float ProjectileSpeed = 2f;
    //  [SerializeField] private bool isShooting = false;
    //  [SerializeField] private int reloadTime = 60;
    //  [SerializeField] private int TimeToShoot;
    //  [SerializeField] private int Health = 10;

    protected Player playerRef;
    protected float DistanceToPlayer;
    [SerializeField] protected float MinDistanceToPlayer = 50;

    private void Awake()
    {
        playerRef = GameObject.Find("Player").GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DistanceToPlayer = Vector3.Distance(transform.position, playerRef.transform.position);
        Vector3 Distance;
        Distance.x = playerRef.transform.position.x - transform.position.x;
        Distance.y = playerRef.transform.position.y - transform.position.y;
        Distance.z = 0;

        Vector3 ActualDistance = Vector3.Normalize(Distance);

        witchMovement();
        AttackCooldown++;
    }

    private void witchMovement()
    {
        if(DistanceToPlayer > MinDistanceToPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerRef.transform.position, (100*Speed) * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, playerRef.transform.position, Speed * Time.deltaTime);
        }


    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && AttackCooldown > DamageCooldown)
        {
            playerRef.damage(DamageGiven);
            AttackCooldown = 0;
        }
    }
}
