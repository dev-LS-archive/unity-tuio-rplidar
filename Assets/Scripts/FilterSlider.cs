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
    public float defaultValue = 5f;
    private void Start()
    {
        filterSlider.value = PlayerPrefs.GetFloat("Filter", defaultValue);
    }

    public void Reset()
    {
        PlayerPrefs.SetFloat("Filter", defaultValue);
        filterSlider.value = PlayerPrefs.GetFloat("Filter");
    }

    public void FilterControl()
    {
        foreach (var spawner in spawners)
        {
            spawner.filterLevel = (int) filterSlider.value;
        }
        PlayerPrefs.SetFloat("Filter", filterSlider.value);
        currentLevel.text = filterSlider.value.ToString(CultureInfo.InvariantCulture);
    }
}
