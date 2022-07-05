using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageSlider : MonoBehaviour
{
    public enum ColorState
    {
        Red,
        Green,
        Blue,
        Alpha
    }
    
    public Image image;
    public Image previewImage;
    
    public Slider scaleSlider;
    public TMP_Text currentText;

    public ColorState colorState;
    public float defaultValue = 1f;
    private void Start()
    {
        switch (colorState)
        {
            case ColorState.Red:
                scaleSlider.value = PlayerPrefs.GetFloat("Red", defaultValue);
                break;
            case ColorState.Green:
                scaleSlider.value = PlayerPrefs.GetFloat("Green", defaultValue);
                break;
            case ColorState.Blue:
                scaleSlider.value = PlayerPrefs.GetFloat("Blue", defaultValue);
                break;
            case ColorState.Alpha:
                scaleSlider.value = PlayerPrefs.GetFloat("Alpha", defaultValue);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Reset()
    {
        switch (colorState)
        {
            case ColorState.Red:
                PlayerPrefs.SetFloat("Red", defaultValue);
                break;
            case ColorState.Green:
                PlayerPrefs.SetFloat("Green", defaultValue);
                break;
            case ColorState.Blue:
                PlayerPrefs.SetFloat("Blue", defaultValue);
                break;
            case ColorState.Alpha:
                PlayerPrefs.SetFloat("Alpha", defaultValue);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        RuntimeChange(previewImage);
    }

    public void RuntimeChange()
    {
        previewImage.color = new Color(PlayerPrefs.GetFloat("Red"), PlayerPrefs.GetFloat("Green"),
            PlayerPrefs.GetFloat("Blue"), PlayerPrefs.GetFloat("Alpha"));
        if (image.color != previewImage.color)
        {
            //print("Change");
            switch (colorState)
            {
                case ColorState.Red:
                    scaleSlider.value = image.color.r;
                    break;
                case ColorState.Green:
                    scaleSlider.value = image.color.g;
                    break;
                case ColorState.Blue:
                    scaleSlider.value = image.color.b;
                    break;
                case ColorState.Alpha:
                    scaleSlider.value = image.color.a;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
    public void RuntimeChange(Image colorImage)
    {
        previewImage.color = new Color(PlayerPrefs.GetFloat("Red"), PlayerPrefs.GetFloat("Green"),
            PlayerPrefs.GetFloat("Blue"), PlayerPrefs.GetFloat("Alpha"));
        if (image.color != previewImage.color)
        {
            //print("Change");
            switch (colorState)
            {
                case ColorState.Red:
                    scaleSlider.value = colorImage.color.r;
                    break;
                case ColorState.Green:
                    scaleSlider.value = colorImage.color.g;
                    break;
                case ColorState.Blue:
                    scaleSlider.value = colorImage.color.b;
                    break;
                case ColorState.Alpha:
                    scaleSlider.value = colorImage.color.a;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
    public void ImageColor()
    {
        switch (colorState)
        {
            case ColorState.Red:
                image.color = new Color(scaleSlider.value, image.color.g, image.color.b, image.color.a);
                previewImage.color = new Color(scaleSlider.value, image.color.g, image.color.b, image.color.a);
                PlayerPrefs.SetFloat("Red", scaleSlider.value);
                break;
            case ColorState.Green:
                image.color = new Color(image.color.r, scaleSlider.value, image.color.b, image.color.a);
                previewImage.color = new Color(image.color.r, scaleSlider.value, image.color.b, image.color.a);
                PlayerPrefs.SetFloat("Green", scaleSlider.value);
                break;
            case ColorState.Blue:
                image.color = new Color(image.color.r, image.color.g, scaleSlider.value, image.color.a);
                previewImage.color = new Color(image.color.r, image.color.g, scaleSlider.value, image.color.a);
                PlayerPrefs.SetFloat("Blue", scaleSlider.value);
                break;
            case ColorState.Alpha:
                image.color = new Color(image.color.r, image.color.g, image.color.b, scaleSlider.value);
                previewImage.color = new Color(image.color.r, image.color.g, image.color.b, scaleSlider.value);
                PlayerPrefs.SetFloat("Alpha", scaleSlider.value);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        currentText.text = Math.Round(scaleSlider.value, 3).ToString(CultureInfo.InvariantCulture);
    }
}
