using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System;
using UnityEngine;

public class BackgroundsChanger : MonoBehaviour
{
    [Header("GUI")]
    private float windowDpi;
    public GameObject[] SceneObjects;
    private int Prefab;
    private int ActiveObject;
    public bool GUIswitcher = false;
    public float yPos = 5f;

    void Start()
    {
        if (Screen.dpi < 1) windowDpi = 1;
        if (Screen.dpi < 200) windowDpi = 1;
        else windowDpi = Screen.dpi / 200f;
        Counter(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            GUIswitcher = !GUIswitcher;
    }

    private void OnGUI()
    {
        if (GUIswitcher)
        {
            if (GUI.Button(new Rect(5 * windowDpi, yPos * windowDpi, 110 * windowDpi, 35 * windowDpi), "Previous effect"))
            {
                Counter(-1);
            }
            if (GUI.Button(new Rect(120 * windowDpi, yPos * windowDpi, 110 * windowDpi, 35 * windowDpi), "Play again"))
            {
                Counter(0);
            }
            if (GUI.Button(new Rect(235 * windowDpi, yPos * windowDpi, 110 * windowDpi, 35 * windowDpi), "Next effect"))
            {
                Counter(+1);
            }
        }
    }

    void Counter(int count)
    {
        Prefab += count;
        if (Prefab > SceneObjects.Length - 1)
        {
            Prefab = 0;
        }
        else if (Prefab < 0)
        {
            Prefab = SceneObjects.Length - 1;
        }
        if (SceneObjects[ActiveObject].activeInHierarchy)
        {
            SceneObjects[ActiveObject].SetActive(false);
        }
        ActiveObject = Prefab;
        SceneObjects[Prefab].SetActive(true);
    }
}