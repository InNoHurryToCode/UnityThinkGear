using UnityEngine;

public class UnityThinkGear
{
    #if UNITY_IOS && !UNITY_EDITOR
    [DllImport ("__Internal")] private static extern void TGAM_Init(bool rawEnabled);
    [DllImport ("__Internal")] private static extern void TGAM_Close();
    [DllImport ("__Internal")] private static extern void TGAM_StartStream();
    [DllImport ("__Internal")] private static extern void TGAM_StopStream();
    [DllImport ("__Internal")] private static extern void TGAM_ScanDevice();
    [DllImport ("__Internal")] private static extern void TGAM_ConnectDevice(string deviceId);
    [DllImport ("__Internal")] private static extern bool TGAM_GetSendRawEnable();
    [DllImport ("__Internal")] private static extern bool TGAM_GetSendEEGEnable();
    [DllImport ("__Internal")] private static extern bool TGAM_GetSendESenseEnable();
    [DllImport ("__Internal")] private static extern bool TGAM_GetSendBlinkEnable();
    [DllImport ("__Internal")] private static extern void TGAM_SetSendRawEnable(bool enabled);
    [DllImport ("__Internal")] private static extern void TGAM_SetSendEEGEnable(bool enabled);
    [DllImport ("__Internal")] private static extern void TGAM_SetSendESenseEnable(bool enabled);
    [DllImport ("__Internal")] private static extern void TGAM_SetSendBlinkEnable(bool enabled);
    #elif UNITY_ANDROID && !UNITY_EDITOR
    private static AndroidJavaClass jc;
    private static AndroidJavaObject jo;
    #endif

    public static void Init(bool rawEnabled)
    {
        #if UNITY_IOS && !UNITY_EDITOR
        TGAM_Init(rawEnabled);
        #elif UNITY_ANDROID && !UNITY_EDITOR
        jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Set<bool>("sendRawEnable", rawEnabled);
        #endif
    }

    public static void Close()
    {
        #if UNITY_IOS && !UNITY_EDITOR
        TGAM_Close();
        #elif UNITY_ANDROID && !UNITY_EDITOR
        jo.Call("disconnect");
        #endif
    }

    public static void StartStream()
    {
        #if UNITY_IOS && !UNITY_EDITOR
        TGAM_StartStream();
        #elif UNITY_ANDROID && !UNITY_EDITOR
        jo.Call("connectWithRaw");
        #endif
    }

    public static void StopStream()
    {
        #if UNITY_IOS && !UNITY_EDITOR
        TGAM_StopStream();
        #elif UNITY_ANDROID && !UNITY_EDITOR
        jo.Call("disconnect");
        #endif
    }

    public static void ScanDevice()
    {
        #if UNITY_IOS && !UNITY_EDITOR
        TGAM_ScanDevice();
        #endif
    }

    public static void ConnectDevice(string deviceId)
    {
        #if UNITY_IOS && !UNITY_EDITOR
        TGAM_ConnectDevice(deviceId);
        #endif
    }

    public static bool GetSendRawEnable()
    {
        #if UNITY_IOS && !UNITY_EDITOR
        return TGAM_GetSendRawEnable();
        #elif UNITY_ANDROID && !UNITY_EDITOR
        return jo.Get<bool>("sendRawEnable");
        #else
        return false;
        #endif
    }

    public static bool GetSendEEGEnable()
    {
        #if UNITY_IOS && !UNITY_EDITOR
        return TGAM_GetSendEEGEnable();
        #elif UNITY_ANDROID && !UNITY_EDITOR
        return jo.Get<bool>("sendEEGEnable");
        #else
        return false;
        #endif
    }

    public static bool GetSendESenseEnable()
    {
        #if UNITY_IOS && !UNITY_EDITOR
        return TGAM_GetSendESenseEnable();
        #elif UNITY_ANDROID && !UNITY_EDITOR
        return jo.Get<bool>("sendESenseEnable");
        #else
        return false;
        #endif
    }

    public static bool GetSendBlinkEnable()
    {
        #if UNITY_IOS && !UNITY_EDITOR
        return TGAM_GetSendBlinkEnable();
        #elif UNITY_ANDROID && !UNITY_EDITOR
        return jo.Get<bool>("sendBlinkEnable");
        #else
        return false;
        #endif
    }
	
    public static void SetSendRawEnable(bool enabled)
    {
        #if UNITY_IOS && !UNITY_EDITOR
        TGAM_SetSendRawEnable(enabled);
        #elif UNITY_ANDROID && !UNITY_EDITOR
        jo.Set<bool>("sendRawEnable", enabled);
        #endif
    }

    public static void SetSendEEGEnable(bool enabled)
    {
        #if UNITY_IOS && !UNITY_EDITOR
        TGAM_SetSendEEGEnable(enabled);
        #elif UNITY_ANDROID && !UNITY_EDITOR
        jo.Set<bool>("sendEEGEnable", enabled);
        #endif
    }

    public static void SetSendESenseEnable(bool enabled)
    {
        #if UNITY_IOS && !UNITY_EDITOR
        TGAM_SetSendESenseEnable(enabled);
        #elif UNITY_ANDROID && !UNITY_EDITOR
        jo.Set<bool>("sendESenseEnable", enabled);
        #endif
    }

    public static void SetSendBlinkEnable(bool enabled)
    {
        #if UNITY_IOS && !UNITY_EDITOR
        TGAM_SetSendBlinkEnable(enabled);
        #elif UNITY_ANDROID && !UNITY_EDITOR
        jo.Set<bool>("sendBlinkEnable", enabled);
        #endif
    }
}
