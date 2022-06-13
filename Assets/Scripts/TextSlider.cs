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
    
    public void FontSize()
    {
        text.fontSize = (int) sizeSlider.value;
        currentText.text = Math.Round(sizeSlider.value, 3).ToString(CultureInfo.InvariantCulture);
    }
}
