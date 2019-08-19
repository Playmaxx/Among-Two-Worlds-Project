using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider HealthSlider;
    [SerializeField] private float Regeneration;
    [SerializeField] private bool Refilling;
    Player playerRef;


    // Start is called before the first frame update
    private void Awake()
    {
        HealthSlider = GetComponent<Slider>();
        playerRef = GameObject.Find("Player").GetComponent<Player>();
    }


    void Start()
    {
        HealthSlider.maxValue = playerRef.health;

    }

    // Update is called once per frame
    void Update()
    {
        //HealthUpdate();
        HealthSlider.value = playerRef.health;
    }

    void HealthUpdate()
    {
        if (Refilling == true)
        {
            HealthSlider.value = HealthSlider.value + (Regeneration * Time.deltaTime);
            if (HealthSlider.value >= 1)
            {
                Refilling = false;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            HealthSlider.value = HealthSlider.value - 1f;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Refilling = true;
        }
    }
}
