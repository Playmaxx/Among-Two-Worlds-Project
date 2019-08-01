﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpPanels : MonoBehaviour
{
    public RectTransform panel_jump;
    public RectTransform panel_worldshifting;
    public CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Awake()
    {
        //canvasGroup = GetComponent<CanvasGroup>
        panel_jump = GetComponent<RectTransform>();
        panel_worldshifting = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Hide()
    {
        canvasGroup.alpha = 0f; //this makes everything transparent
        canvasGroup.blocksRaycasts = false; //this prevents the UI element to receive input events
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
