using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
public class TimeManager : MonoBehaviour
{
    public Slider timelineSpeed;
    public PlayableDirector pd;
    public TMP_Text currentText;
    public TMP_Text playTime, playLength;

    private void Start()
    {
        var time = Math.Round(pd.playableAsset.duration, 1);
        playLength.text = ReturnTime(time);
    }

    private void Update()
    {
        var time = Math.Round(pd.time, 1);
        playTime.text = ReturnTime(time);
    }

    public void TimelineSpeed()
    {
        currentText.text = Math.Round(timelineSpeed.value, 3).ToString(CultureInfo.InvariantCulture);
        pd.Pause();
        pd.playableGraph.GetRootPlayable(0).SetSpeed(timelineSpeed.value);
        pd.Play();
    }
    string ReturnTime(double time)
    {
        var minute = time / 60;
        var second = time % 60;
        var ts = TimeSpan.FromSeconds(time);

        return ts.ToString(@"mm") + " : " + ts.ToString(@"ss");
    }
}
