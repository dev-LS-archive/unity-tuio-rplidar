using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TouchScript.InputSources;
public class EndTimeReservation : MonoBehaviour
{
    public GameObject controlWindow;
    public InputField quitHour;
    public InputField quitMinute;
    public InputField quitSecond;
    public InputField quitAmpm;

    public TuioInput tiInput;
    public bool isWindowOpen = false;
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
                tiInput.enabled = false;
                Save_And_Close();
            }
            else
            {
                tiInput.enabled = true;
                controlWindow.SetActive(true);
                isWindowOpen = true;
            }
            
        }
        
        Debug.Log(DateTime.Now.ToString("h:m:s tt"));
        Debug.Log(quitHour.text+":"+quitMinute.text+":"+quitSecond.text+" "+quitAmpm.text);
        if (DateTime.Now.ToString("h:m:s tt") == quitHour.text+":"+quitMinute.text+":"+quitSecond.text+" "+quitAmpm.text)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
    public void Save_And_Close()
    {
        PlayerPrefs.SetString("Hour", quitHour.text);
        PlayerPrefs.SetString("Minute", quitMinute.text);
        PlayerPrefs.SetString("Second", quitSecond.text);
        PlayerPrefs.SetString("AM/PM", quitAmpm.text);

        SetTime();

        controlWindow.SetActive(false);
        isWindowOpen = false;
    }

    void SetTime()
    {
        string hour = PlayerPrefs.GetString("Hour", 5.ToString());
        quitHour.text = hour;

        string minute = PlayerPrefs.GetString("Minute", 35.ToString());
        quitMinute.text = minute;

        string second = PlayerPrefs.GetString("Second", 0.ToString());
        quitSecond.text = second;
        
        string ampm = PlayerPrefs.GetString("AM/PM", "P");
        if(ampm=="A")
            quitAmpm.text = "오전";
        if(ampm=="P")
            quitAmpm.text = "오후";
        
    }
}
