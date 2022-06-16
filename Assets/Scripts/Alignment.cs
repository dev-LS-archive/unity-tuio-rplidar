using UnityEngine;
using UnityEngine.UI;

public class Alignment : MonoBehaviour
{
    public Text text;
    
    private void AligmentCon(TextAnchor anchor)
    {
        text.alignment = anchor;
    }

    public void AligmentLeft()
    {
        AligmentCon(TextAnchor.LowerLeft);
    }
    
    public void AligmentCenter()
    {
        AligmentCon(TextAnchor.LowerCenter);
    }
    public void AligmentRight()
    {
        AligmentCon(TextAnchor.LowerRight);
    }
}
