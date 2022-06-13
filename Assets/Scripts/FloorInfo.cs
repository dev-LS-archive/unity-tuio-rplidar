using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloorInfo : MonoBehaviour
{
    public static FloorInfo Instance = null;
    public Transform floorInfo;
    [SerializeField] private float floorCool = 0f;
    [SerializeField] private float floorTime = 5f;
    
    public Slider sizeSlider;
    public TMP_Text currentText;
    
    public void FloorSlider()
    {
        floorTime = sizeSlider.value;
        currentText.text = Math.Round(sizeSlider.value, 3).ToString(CultureInfo.InvariantCulture);
    }
    
    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    private void Update()
    {
        if (floorInfo.gameObject.activeSelf != true)
        {
            floorCool += Time.deltaTime;
            if (floorCool > floorTime)
            {
                floorCool = 0f;
                floorInfo.gameObject.SetActive(true);
            }
        }
    }

    public void HideInfo()
    {
        floorCool = 0f;
        floorInfo.gameObject.gameObject.SetActive(false);
    }
}
