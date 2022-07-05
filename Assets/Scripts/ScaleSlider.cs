using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScaleSlider : MonoBehaviour
{
    public RectTransform scale;
    public Slider sizeSliderX, sizeSliderY;
    public TMP_Text currentTextX, currentTextY;
    public float defaultValueX = 3f, defaultValueY = 4.2f;

    private void Start()
    {
        //print(PlayerPrefs.GetFloat("X_Scale") + "/" + PlayerPrefs.GetFloat("Y_Scale"));
        Fix();
        //print(PlayerPrefs.GetFloat("X_Scale") + "/" + PlayerPrefs.GetFloat("Y_Scale"));
    }

    void Fix()
    {
        sizeSliderX.value = PlayerPrefs.GetFloat("X_Scale", defaultValueX);
        sizeSliderY.value = PlayerPrefs.GetFloat("Y_Scale", defaultValueY);
        scale.localScale = new Vector3(sizeSliderX.value, sizeSliderY.value, 1);
    }
    public void Reset()
    {
        PlayerPrefs.SetFloat("X_Scale", defaultValueX);
        PlayerPrefs.SetFloat("Y_Scale", defaultValueY);
        Fix();
    }

    public void ScaleSizeX()
    {
        scale.localScale = new Vector3(sizeSliderX.value, sizeSliderY.value, 1);
        PlayerPrefs.SetFloat("X_Scale", sizeSliderX.value);
        currentTextX.text = Math.Round(sizeSliderX.value, 3).ToString(CultureInfo.InvariantCulture);
    }
    public void ScaleSizeY()
    {
        scale.localScale = new Vector3(sizeSliderX.value, sizeSliderY.value, 1);
        PlayerPrefs.SetFloat("Y_Scale", sizeSliderY.value);
        currentTextY.text = Math.Round(sizeSliderY.value, 3).ToString(CultureInfo.InvariantCulture);
    }
}
