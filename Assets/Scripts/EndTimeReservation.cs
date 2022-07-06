using System;
using TMPro;
using TouchScript.Examples.RawInput;
using TouchScript.InputSources;
using TouchScript.Layers.UI;
using UnityEngine;
using UnityEngine.UI;
public class EndTimeReservation : MonoBehaviour
{
    public GameObject controlWindow;
    public Dropdown quitHour;
    public Dropdown quitMinute;
    public Dropdown quitSecond;
    public Dropdown quitAmpm;
    
    public RandomSpawner[] spawners;
    public TouchScriptInputModule touchModule;
    //public GameObject touchManager;

    public TMP_Text h, m, s, t;
    public bool isWindowOpen = false;

    private int _hour, _minute, _second, _ampm;

    public string dateTime = null;
    // Start is called before the first frame update
    private void Start()
    {
        SetTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            
            if (controlWindow.activeSelf)
            {
                foreach (var variable in spawners)
                {
                    variable.enabled = true;
                }
                touchModule.enabled = true;
                //touchManager.SetActive(true);
                Save_And_Close();
            }
            else
            {
                foreach (var variable in spawners)
                {
                    variable.enabled = false;
                }
                touchModule.enabled = false;
                //touchManager.SetActive(false);
                controlWindow.SetActive(true);
                isWindowOpen = true;
            }
            
        }

        h.text = DateTime.Now.ToString("hh");
        m.text = DateTime.Now.ToString("mm");
        s.text = DateTime.Now.ToString("ss");
        t.text = DateTime.Now.ToString("tt");
        
        dateTime = quitHour.options[_hour].text + ":" + quitMinute.options[_minute].text + ":" +
                   quitSecond.options[_second].text + " " + quitAmpm.options[_ampm].text;
        //Debug.Log(DateTime.Now.ToString("h:m:s tt"));
        //Debug.Log(dateTime);
        if (DateTime.Now.ToString("h:m:s tt") == dateTime)
        {
            Quit();
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void Save_And_Close()
    {
        PlayerPrefs.SetInt("Hour", quitHour.value);
        PlayerPrefs.SetInt("Minute", quitMinute.value);
        PlayerPrefs.SetInt("Second", quitSecond.value);
        PlayerPrefs.SetInt("AM/PM", quitAmpm.value);

        SetTime();

        controlWindow.SetActive(false);
        isWindowOpen = false;
    }

    void SetTime()
    {
        _hour = PlayerPrefs.GetInt("Hour", 4);
        quitHour.value = _hour;

        _minute = PlayerPrefs.GetInt("Minute", 35);
        quitMinute.value = _minute;

        _second = PlayerPrefs.GetInt("Second", 0);
        quitSecond.value = _second;

        _ampm = PlayerPrefs.GetInt("AM/PM", 1);
        quitAmpm.value = _ampm;
    }
}
