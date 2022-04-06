using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;


public class MouseControl : MonoBehaviour
{
    static MouseControl instance;
    public static MouseControl Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MouseControl>();
            }

            return instance;
        }

    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

    [DllImport("user32")]
    public static extern int SetCursorPos(int x, int y);

    private const int kMouseEventMove = 0x0001; /* mouse move */
    private const int kMouseEventLeftDown = 0x0002; /* left button down */
    private const int kMouseEventLeftUp = 0x0004; /* left button up */
    private const int kMouseEventRightDown = 0x0008; /* right button down */

    // private readonly ManualResetEvent stoppeing_event_ = new ManualResetEvent(false);
    TimeSpan interval_;

    public Queue<Vector3> myQueue;

    public bool isQueueStart;

    public void Start()
    {
        myQueue = new Queue<Vector3>();

    }

    private void Update()
    {
        if (myQueue.Count >= 1 && !isQueueStart)
        {
            isQueueStart = true;

            Vector3 pos = myQueue.Dequeue();


            MC_Touch((int)pos.x, (int)pos.y);

            /*
            if (!SizeControl.Instance.isWindowOpen)
               

            */

        }
    }


    public void MC_Touch(int X, int Y)
    {
        MouseSetPosCustom(X, Y);
        Debug.Log("ÅÍÄ¡ x :" + X + ", y : " + Y);
        StartCoroutine("MouseClickCustom");
    }

    public void MC_Move(int X, int Y)
    {
        MouseSetPosCustom(X, Y);
        //  StartCoroutine("MouseClickCustom");
    }

    IEnumerator MouseClickCustom()
    {
        mouse_event(kMouseEventLeftDown, 0, 0, 0, 0);
        yield return new WaitForSeconds(0.05f);
        mouse_event(kMouseEventLeftUp, 0, 0, 0, 0);
        isQueueStart = false;
    }

    public void MouseSetPosCustom(int x, int y, int interval = 100)
    {
        try
        {
            SetCursorPos(x, y);
            //  stoppeing_event_.WaitOne(interval_);

            //   MouseClickCustom(interval);
        }
        catch (Exception e)
        {
            //  MessageBox.Show("MouseSetPosCustom\r\n" + e.Message);
        }
    }
}
