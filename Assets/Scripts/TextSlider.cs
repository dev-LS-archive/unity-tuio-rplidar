using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextSlider : MonoBehaviour
{
    public Text text;
    public Slider sizeSlider;
    public TMP_Text currentText;
    public float defaultValue = 30f;
    
    private void Start()
    {
        sizeSlider.value = PlayerPrefs.GetFloat("FontSize", defaultValue);
    }
    public void Reset()
    {
        PlayerPrefs.SetFloat("FontSize", defaultValue);
        sizeSlider.value = PlayerPrefs.GetFloat("FontSize");
    }
    public void FontSize()
    {
        text.fontSize = (int) sizeSlider.value;
        PlayerPrefs.SetFloat("FontSize", sizeSlider.value);
        currentText.text = Math.Round(sizeSlider.value, 3).ToString(CultureInfo.InvariantCulture);
    }
}
