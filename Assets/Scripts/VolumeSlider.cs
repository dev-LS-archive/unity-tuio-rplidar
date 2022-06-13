using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public PostProcessVolume volume;
    public Slider volumeSlider;
    public TMP_Text currentText;
    
    public void VolumeWeight()
    {
        volume.weight = volumeSlider.value;
        currentText.text = Math.Round(volumeSlider.value, 3).ToString(CultureInfo.InvariantCulture);
    }
}
