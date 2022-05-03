using System;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;
using TouchScript.Gestures;
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

    public string actFile;
    public string fileName;
    public string path;
    void ActivateApp(string processName, int cmdShow)
    {
        Process[] p = Process.GetProcessesByName(processName);
 
        // Activate the first application we find with this name
        if (p.Count() > 0)
 
            try
            {
                ShowWindowAsync(p[0].MainWindowHandle, cmdShow);
                AllowSetForegroundWindow(p[0].Id);
                SetForegroundWindow(p[0].MainWindowHandle);
            }
            catch(Exception ex)
            {
                //Debug.WriteLine(ex.Message);
                ReleaseGesture.print(ex.Message);
            }
    }   
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        UnActProgram();
        ActProgram();
        //SetForegroundWindow(GetActiveWindow());
        SceneManager.LoadScene(1);
        ActWindow(actFile,SW_SHOWMINIMIZED);
        ActWindow(Application.productName, SW_SHOWMAXIMIZED);
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

    private void ActWindow(string processName, int cmdShow)
    {
        foreach (Process process in Process.GetProcesses())
        {
            if (process.ProcessName.StartsWith(processName))//actFile
            {
                ActivateApp(process.ProcessName,cmdShow);
                SetForegroundWindow(process.Handle);
            }
        }
    }

    private void ActProgram()
    {
        //바탕화면
        path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        Process.Start(path + "/" + actFile);
    }

    private void UnActProgram()
    {
        foreach (Process process in Process.GetProcesses())
        {
            if (process.ProcessName.StartsWith(fileName))
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
