using System;
using System.Globalization;
using TMPro;
using TouchScript.Examples.RawInput;
using UnityEngine;
using UnityEngine.UI;

public class FilterSlider : MonoBehaviour
{
    public RandomSpawner[] spawners;
    public Slider filterSlider;
    public TMP_Text currentLevel;
    
    public void FilterControl()
    {
        foreach (var spawner in spawners)
        {
            spawner.filterLevel = (int) filterSlider.value;
        }
        currentLevel.text = filterSlider.value.ToString(CultureInfo.InvariantCulture);
    }
}
