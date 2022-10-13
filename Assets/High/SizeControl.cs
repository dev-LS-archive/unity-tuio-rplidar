using System.Collections;
using System.Collections.Generic;
using TouchScript.Layers.UI;
using UnityEngine;
using UnityEngine.UI;

public class SizeControl : MonoBehaviour
{
    public TouchScriptInputModule touchModule;
    public RoseManager roseManager;

    static SizeControl instance;

    public static SizeControl Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SizeControl>();
            }
            return instance;
        }
    }

    public GameObject controlWindow;

    public RectTransform bottom;
    public RectTransform left;
    public RectTransform right;

    public Slider slider_bottom;
    public Slider slider_left;
    public Slider slider_right;

    public bool isWindowOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        float myBottom = PlayerPrefs.GetFloat("BOTTOM", 0);

        slider_bottom.value = myBottom;

        float myLeft = PlayerPrefs.GetFloat("Left", 0);

        slider_left.value = myLeft;

        float myRight = PlayerPrefs.GetFloat("Right", 0);

        slider_right.value = myRight;
    }

    // Update is called once per frame
    void Update()
    {


        bottom.sizeDelta = new Vector2(1920, slider_bottom.value * 1080) ;
        left.sizeDelta = new Vector2(slider_left.value * 1000 , 1080);
        right.sizeDelta = new Vector2(slider_right.value * 1000 , 1080);


        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (controlWindow.activeSelf)
            {
                roseManager.enabled = true;
                
                touchModule.enabled = true;
                //touchManager.SetActive(true);
                Save_And_Close();
            }
            else
            {
                roseManager.enabled = false;
                
                touchModule.enabled = false;
                //touchManager.SetActive(false);
                controlWindow.SetActive(true);
                isWindowOpen = true;
            }
            
        }

    }

    public void Save_And_Close()
    {

        PlayerPrefs.SetFloat("BOTTOM", slider_bottom.value);
        PlayerPrefs.SetFloat("Left", slider_left.value);
        PlayerPrefs.SetFloat("Right", slider_right.value);

        controlWindow.SetActive(false);
        isWindowOpen = false;

    }
}
