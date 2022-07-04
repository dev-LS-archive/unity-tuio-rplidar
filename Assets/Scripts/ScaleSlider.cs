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
    
    public void ScaleSize()
    {
        scale.localScale = new Vector3(sizeSliderX.value, sizeSliderY.value, 1);
        currentTextX.text = Math.Round(sizeSliderX.value, 3).ToString(CultureInfo.InvariantCulture);
        currentTextY.text = Math.Round(sizeSliderY.value, 3).ToString(CultureInfo.InvariantCulture);
    }
}
