using UnityEngine;
using UnityEngine.UI;

public class Alignment : MonoBehaviour
{
    public Text text;
    
    private void Start()
    {
        switch (PlayerPrefs.GetString("Alignment","Center"))
        {
            case "Left":
                AlignmentLeft();
                break;
            case "Center":
                AlignmentCenter();
                break;
            case "Right":
                AlignmentRight();
                break;
        }
    }
    private void AlignmentCon(TextAnchor anchor)
    {
        text.alignment = anchor;
    }

    public void AlignmentLeft()
    {
        PlayerPrefs.SetString("Alignment", "Left");
        AlignmentCon(TextAnchor.LowerLeft);
    }
    
    public void AlignmentCenter()
    {
        PlayerPrefs.SetString("Alignment", "Center");
        AlignmentCon(TextAnchor.LowerCenter);
    }
    public void AlignmentRight()
    {
        PlayerPrefs.SetString("Alignment", "Right");
        AlignmentCon(TextAnchor.LowerRight);
    }
}
