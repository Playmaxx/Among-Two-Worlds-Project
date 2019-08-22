using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLifebarUI : MonoBehaviour
{
    [SerializeField] private Image m_darkUI;
    [SerializeField] private Image m_lightUI;
 

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GMInstance.currentdim == GameManager.dimension.Light)
        {
            m_lightUI.enabled = true;
            m_darkUI.enabled = false;
        }
        else
        {
            m_lightUI.enabled = false;
            m_darkUI.enabled = true;
        }
    }
}