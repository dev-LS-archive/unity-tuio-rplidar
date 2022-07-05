using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManger : MonoBehaviour
{
    // Start is called before the first frame update
    public CanvasGroup cg;
    void Awake()
    {
        cg.interactable = false;
        cg.alpha = 0;
    }

    private void Start()
    {
        if (cg.gameObject.activeSelf == false)
            cg.gameObject.SetActive(true);
        Invoke(nameof(Off), 0.1f);
    }

    void Off()
    {
        if (cg.gameObject.activeSelf == true)
            cg.gameObject.SetActive(false);
        cg.alpha = 1;
        cg.interactable = true;
    }
}
