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
        playLength.text = Math.Round(pd.playableAsset.duration, 3).ToString(CultureInfo.InvariantCulture);
    }

    private void Update()
    {
        playTime.text=Math.Round(pd.time, 3).ToString(CultureInfo.InvariantCulture);
    }

    public void TimelineSpeed()
    {
        currentText.text = Math.Round(timelineSpeed.value, 3).ToString(CultureInfo.InvariantCulture);
        pd.Pause();
        pd.playableGraph.GetRootPlayable(0).SetSpeed(timelineSpeed.value);
        pd.Play();
    }
}
