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

    //private const int SW_HIDE = 0;
    //private const int SW_SHOWNORMAL = 1;
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
        if (p.Any())
        {
            try
            {
                ShowWindowAsync(p[0].MainWindowHandle, cmdShow);
                AllowSetForegroundWindow(p[0].Id);
                SetForegroundWindow(p[0].MainWindowHandle);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                //ReleaseGesture.print(ex.Message);
            }
        }
    }   
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        UnActProgram();
        ActProgram();
        ActWindow(fileName,SW_SHOWMINIMIZED);
        //SetForegroundWindow(GetActiveWindow());
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
        {
            ActWindow(Application.productName, SW_SHOWMAXIMIZED);
            Foreground();//Invoke(nameof(Foreground), 1f);
        }
    }

    void Foreground()
    {
        SetForegroundWindow(GetActiveWindow());
    }

    private void ActWindow(string processName, int cmdShow)
    {
        //print(processName);
        foreach (Process process in Process.GetProcesses())
        {
            if (process.ProcessName.StartsWith(processName))//actFile
            {
                ActivateApp(process.ProcessName,cmdShow);
            }
        }
    }

    private void ActProgram()
    {
        //바탕화면
        path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        Process.Start(path + "/" + actFile);
        foreach (Process process in Process.GetProcesses())
        {
            if (process.ProcessName.StartsWith(actFile))//actFile
            {
                ActivateApp(process.ProcessName,6);
            }
        }
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
