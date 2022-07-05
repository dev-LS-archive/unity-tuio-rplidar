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
    
    public Slider timeSlider;
    public TMP_Text currentText;
    public float defaultValue = 5f;
    public void FloorSlider()
    {
        floorTime = timeSlider.value;
        PlayerPrefs.SetFloat("CoolTime", timeSlider.value);
        currentText.text = Math.Round(timeSlider.value, 3).ToString(CultureInfo.InvariantCulture);
    }
    
    public void Reset()
    {
        PlayerPrefs.SetFloat("CoolTime", defaultValue);
        timeSlider.value = PlayerPrefs.GetFloat("CoolTime");
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
    private void Start()
    {
        timeSlider.value = PlayerPrefs.GetFloat("CoolTime", defaultValue);
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
