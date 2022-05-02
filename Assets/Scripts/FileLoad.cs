using System;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = System.Diagnostics.Debug;

public class FileLoad : MonoBehaviour
{
    [DllImport("user32.dll")]
    private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
    private const int SW_SHOWNORMAL = 1;
    private const int SW_SHOWMINIMIZED = 2;
    private const int SW_SHOWMAXIMIZED = 3;
    
    [DllImport("user32.dll")]
    static extern bool SetForegroundWindow(IntPtr hWnd);
    [DllImport("user32.dll")]
    static extern bool AllowSetForegroundWindow(int dwProcessId);

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    void ActivateApp(string processName)
    {
        Process[] p = Process.GetProcessesByName(processName);
 
        // Activate the first application we find with this name
        if (p.Count() > 0)
 
            try
            {
                ShowWindowAsync(p[0].MainWindowHandle, SW_SHOWMINIMIZED);
                AllowSetForegroundWindow(p[0].Id);
                SetForegroundWindow(p[0].MainWindowHandle);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
    }   
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        ActProgram();
        MinWindow();
        SetForegroundWindow(GetActiveWindow());
        SceneManager.LoadScene(1);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
            Invoke(nameof(Foreground), 1f);
    }

    void Foreground()
    {
        SetForegroundWindow(GetActiveWindow());
    }
    private void MinWindow()
    {
        foreach (Process process in Process.GetProcesses())
        {
            if (process.ProcessName.StartsWith("MultiTouch_Server"))
            {
                ActivateApp(process.ProcessName);
            }
        }
    }

    private void ActProgram()
    {
        Process.Start(@"C:\Users\admin\Desktop\MultiTouch_Server.exe - 바로 가기.lnk");
    }

    private void UnActProgram()
    {
        foreach (Process process in Process.GetProcesses())
        {
            if (process.ProcessName.StartsWith("MultiTouch_Server"))
            {
                process.Kill();
            }
        }
    }

    private void OnApplicationQuit()
    {
        UnActProgram();
    }
}
