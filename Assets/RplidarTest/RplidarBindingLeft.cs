using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;

using System;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct LidarDataLeft
{
    public byte syncBit;
    public float theta;
    public float distant;
    public uint quality;
};

public class RplidarBindingLeft
{

    static RplidarBindingLeft()
    {
        var currentPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
#if UNITY_EDITOR_64
        currentPath += Path.PathSeparator + Application.dataPath + "/Plugins/x86_64/";
#elif UNITY_EDITOR_32
        currentPath += Path.PathSeparator + Application.dataPath+ "/Plugins/x86/";
#endif
        Environment.SetEnvironmentVariable("PATH", currentPath);
    }


    //¿À¸¥

    [DllImport("RplidarCppLeft.dll")]
    public static extern int OnConnect(string port);

    [DllImport("RplidarCppLeft.dll")]
    public static extern int OnOneConnect(string port);

    [DllImport("RplidarCppLeft.dll")]
    public static extern bool OnDisconnect();

    [DllImport("RplidarCppLeft.dll")]
    public static extern bool StartMotor();
    [DllImport("RplidarCppLeft.dll")]
    public static extern bool EndMotor();

    [DllImport("RplidarCppLeft.dll")]
    public static extern bool StartScan();
    [DllImport("RplidarCppLeft.dll")]
    public static extern bool EndScan();

    [DllImport("RplidarCppLeft.dll")]
    public static extern bool ReleaseDrive();

    [DllImport("RplidarCppLeft.dll")]
    public static extern int GetLDataSize();

    [DllImport("RplidarCppLeft.dll")]
    private static extern void GetLDataSampleArray(IntPtr ptr);

    [DllImport("RplidarCppLeft.dll")]
    private static extern int GrabData(IntPtr ptr);


    // [DllImport("RpLIdarDll.dll")]
    // private static extern void getw();

    public static LidarDataLeft[] GetSampleData()
    {
        var d = new LidarDataLeft[2];
        var handler = GCHandle.Alloc(d, GCHandleType.Pinned);
        GetLDataSampleArray(handler.AddrOfPinnedObject());
        handler.Free();
        return d;
    }

    public static int GetData(ref LidarDataLeft[] data)
    {
        var handler = GCHandle.Alloc(data, GCHandleType.Pinned);
        int count = GrabData(handler.AddrOfPinnedObject());
        handler.Free();

        return count;
    }

}
