using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public PlayableDirector pd;
    public TMP_Text playTime, playLength;
    private double _pdLength;


    private void Start()
    {
        var time = Math.Round(pd.playableAsset.duration, 1);
        _pdLength = time;
        playLength.text = ReturnTime(time);
    }

    private void Update()
    {
        var time = Math.Round(pd.time, 1);
        playTime.text = ReturnTime(_pdLength - time);
    }
    string ReturnTime(double time)
    {
        var minute = time / 60;
        var second = time % 60;
        var ts = TimeSpan.FromSeconds(time);

        return ts.ToString(@"mm") + " : " + ts.ToString(@"ss");
    }
}
