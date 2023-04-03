using UnityEngine;
using UnityEngine.UI;

public class Alignment : MonoBehaviour
{
    public Dropdown dropdown;
    public Text text;
    
    private void Start()
    {
        var opText = PlayerPrefs.GetString("Alignment", "MiddleCenter");
        for (int i = 0; i < dropdown.options.Count; i++)
        {
            if (dropdown.options[i].text == opText)
            {
                dropdown.value = i;
            }
            else
            {
                AlignmentEvent();
            }
        }
    }

    public void AlignmentEvent()
    {
#if UNITY_EDITOR
        //print(dropdown.options[dropdown.value].text);
#endif
        switch (dropdown.options[dropdown.value].text)
        {
            case "UpperLeft":
                AlignmentUpperLeft();
                break;
            case "UpperCenter":
                AlignmentUpperCenter();
                break;
            case "UpperRight":
                AlignmentUpperRight();
                break;
            
            case "MiddleLeft":
                AlignmentMiddleLeft();
                break;
            case "MiddleCenter":
                AlignmentMiddleCenter();
                break;
            case "MiddleRight":
                AlignmentMiddleRight();
                break;
            
            case "LowerLeft":
                AlignmentLowerLeft();
                break;
            case "LowerCenter":
                AlignmentLowerCenter();
                break;
            case "LowerRight":
                AlignmentLowerRight();
                break;
            default:
                AlignmentMiddleCenter();
                break;;
        }
    }
    private void AlignmentCon(TextAnchor anchor)
    {
        text.alignment = anchor;
    }

    private void AlignmentUpperLeft()
    {
        PlayerPrefs.SetString("Alignment", "UpperLeft");
        AlignmentCon(TextAnchor.UpperLeft);
    }

    private void AlignmentUpperCenter()
    {
        PlayerPrefs.SetString("Alignment", "UpperCenter");
        AlignmentCon(TextAnchor.UpperCenter);
    }

    private void AlignmentUpperRight()
    {
        PlayerPrefs.SetString("Alignment", "UpperRight");
        AlignmentCon(TextAnchor.UpperRight);
    }

    private void AlignmentMiddleLeft()
    {
        PlayerPrefs.SetString("Alignment", "MiddleLeft");
        AlignmentCon(TextAnchor.MiddleLeft);
    }

    private void AlignmentMiddleCenter()
    {
        PlayerPrefs.SetString("Alignment", "MiddleCenter");
        AlignmentCon(TextAnchor.MiddleCenter);
    }

    private void AlignmentMiddleRight()
    {
        PlayerPrefs.SetString("Alignment", "MiddleRight");
        AlignmentCon(TextAnchor.MiddleRight);
    }

    private void AlignmentLowerLeft()
    {
        PlayerPrefs.SetString("Alignment", "LowerLeft");
        AlignmentCon(TextAnchor.LowerLeft);
    }

    private void AlignmentLowerCenter()
    {
        PlayerPrefs.SetString("Alignment", "LowerCenter");
        AlignmentCon(TextAnchor.LowerCenter);
    }

    private void AlignmentLowerRight()
    {
        PlayerPrefs.SetString("Alignment", "LowerRight");
        AlignmentCon(TextAnchor.LowerRight);
    }
}
