using UnityEngine;
using System.Collections;

public class ThinkGearController : MonoBehaviour
{
    public delegate void UpdateStringDelegate(string value);
    public delegate void UpdateIntDelegate(int value);
    public delegate void UpdateFloatDelegate(float value);

    #if UNITY_ANDROID
    public event UpdateStringDelegate UpdateConnectStateEvent;
    #elif UNITY_IOS
    public event UpdateStringDelegate UpdateDeviceInfoEvent;
    #endif
    public event UpdateIntDelegate UpdatePoorSignalEvent;
    public event UpdateIntDelegate UpdateAttentionEvent;
    public event UpdateIntDelegate UpdateMeditationEvent;
    public event UpdateIntDelegate UpdateRawdataEvent;
    public event UpdateIntDelegate UpdateBlinkEvent;
    public event UpdateFloatDelegate UpdateDeltaEvent;
    public event UpdateFloatDelegate UpdateThetaEvent;
    public event UpdateFloatDelegate UpdateLowAlphaEvent;
    public event UpdateFloatDelegate UpdateHighAlphaEvent;
    public event UpdateFloatDelegate UpdateLowBetaEvent;
    public event UpdateFloatDelegate UpdateHighBetaEvent;
    public event UpdateFloatDelegate UpdateLowGammaEvent;
    public event UpdateFloatDelegate UpdateHighGammaEvent;
    public event UpdateIntDelegate UpdateAlgoAttentionEvent;
    public event UpdateIntDelegate UpdateAlgoMeditationEvent;
    public event UpdateFloatDelegate UpdateAlgoDeltaEvent;
    public event UpdateFloatDelegate UpdateAlgoThetaEvent;
    public event UpdateFloatDelegate UpdateAlgoAlphaEvent;
    public event UpdateFloatDelegate UpdateAlgoBetaEvent;
    public event UpdateFloatDelegate UpdateAlgoGammaEvent;

    private bool sendRawEnable;
    private bool sendEEGEnable;
    private bool sendESenseEnable;
    private bool sendBlinkEnable;
    private bool checkUpdate;

    void Awake()
    {
        UnityThinkGear.Init(true);
        StartCoroutine(CheckUpdateCoroutine());
    }

    void Start()
    {
        sendRawEnable = UnityThinkGear.GetSendRawEnable();
        sendEEGEnable = UnityThinkGear.GetSendEEGEnable();
        sendESenseEnable = UnityThinkGear.GetSendESenseEnable();
        sendBlinkEnable = UnityThinkGear.GetSendBlinkEnable();
        checkUpdate = true;
    }

    IEnumerator CheckUpdateCoroutine()
    {
        while (checkUpdate)
        {
            if (!sendRawEnable && (UpdateRawdataEvent != null))
            {
                sendRawEnable = true;
            }
            
            if (sendRawEnable && UpdateRawdataEvent == null)
            {
                sendRawEnable = false;
            }

            if (!sendEEGEnable &&
            (UpdateDeltaEvent != null || UpdateThetaEvent != null ||
            UpdateLowAlphaEvent != null || UpdateLowBetaEvent != null ||
            UpdateLowGammaEvent != null || UpdateHighAlphaEvent != null ||
            UpdateHighBetaEvent != null || UpdateHighGammaEvent != null))
            {
                sendEEGEnable = true;
            }
            
            if (sendEEGEnable &&
            UpdateDeltaEvent == null && UpdateThetaEvent == null &&
            UpdateLowAlphaEvent == null && UpdateLowBetaEvent == null &&
            UpdateLowGammaEvent == null && UpdateHighAlphaEvent == null &&
            UpdateHighBetaEvent == null && UpdateHighGammaEvent == null)
            {
                sendEEGEnable = false;
            }

            if (!sendESenseEnable && (UpdateAttentionEvent != null || UpdateMeditationEvent != null))
            {
                sendESenseEnable = true;
            }
            
            if (sendESenseEnable && UpdateAttentionEvent == null && UpdateMeditationEvent == null)
            {
                sendESenseEnable = false;
            }

            if (!sendBlinkEnable && (UpdateBlinkEvent != null))
            {
                sendBlinkEnable = true;
            }
            
            if (sendBlinkEnable && UpdateBlinkEvent == null)
            {
                sendBlinkEnable = false;
            }

            UnityThinkGear.SetSendRawEnable(sendRawEnable);
            UnityThinkGear.SetSendEEGEnable(sendEEGEnable);
            UnityThinkGear.SetSendESenseEnable(sendESenseEnable);
            UnityThinkGear.SetSendBlinkEnable(sendBlinkEnable);

            yield return new WaitForSeconds(1f);
        }
    }

    #if UNITY_ANDROID
    void receiveConnectState1(string value)
    {
        if (UpdateConnectStateEvent != null)
        {
            UpdateConnectStateEvent(value);
        }
    }
    #elif UNITY_IOS
    void receiveDeviceInfo(string value) 
    {
        if (UpdateDeviceInfoEvent != null) 
        {
            UpdateDeviceInfoEvent(value);
        }
    }
    #endif

    void receivePoorSignal(string value)
    {
        if (UpdatePoorSignalEvent != null)
        {
            UpdatePoorSignalEvent(int.Parse(value));
        }
    }

    void receiveAttention(string value)
    {
        if (UpdateAttentionEvent != null)
        {
            UpdateAttentionEvent(int.Parse(value));
        }
    }

    void receiveMeditation(string value)
    {
        if (UpdateMeditationEvent != null)
        {
            UpdateMeditationEvent(int.Parse(value));
        }
    }

    void receiveRawdata(string value)
    {
        if (UpdateRawdataEvent != null)
        {
            UpdateRawdataEvent(int.Parse(value));
        }
    }

    void receiveBlink(string value)
    {
        if (UpdateBlinkEvent != null)
        {
            UpdateBlinkEvent(int.Parse(value));
        }
    }

    void receiveDelta(string value)
    {
        if (UpdateDeltaEvent != null)
        {
            UpdateDeltaEvent(float.Parse(value));
        }
    }

    void receiveTheta(string value)
    {
        if (UpdateThetaEvent != null)
        {
            UpdateThetaEvent(float.Parse(value));
        }
    }

    void receiveLowAlpha(string value)
    {
        if (UpdateLowAlphaEvent != null)
        {
            UpdateLowAlphaEvent(float.Parse(value));
        }
    }

    void receiveHighAlpha(string value)
    {
        if (UpdateHighAlphaEvent != null)
        {
            UpdateHighAlphaEvent(float.Parse(value));
        }
    }

    void receiveLowBeta(string value)
    {
        if (UpdateLowBetaEvent != null)
        {
            UpdateLowBetaEvent(float.Parse(value));
        }
    }

    void receiveHighBeta(string value)
    {
        if (UpdateHighBetaEvent != null)
        {
            UpdateHighBetaEvent(float.Parse(value));
        }
    }

    void receiveLowGamma(string value)
    {
        if (UpdateLowGammaEvent != null)
        {
            UpdateLowGammaEvent(float.Parse(value));
        }
    }

    void receiveHighGamma(string value)
    {
        if (UpdateHighGammaEvent != null)
        {
            UpdateHighGammaEvent(float.Parse(value));
        }
    }

    void receiveEEGAlgorithmValue(string valueKey)
    {
        string[] value = valueKey.Split(':');

        if (value != null && value.Length >= 2)
        {
            switch (value[0])
            {
                case "attention":
                    if (UpdateAlgoAttentionEvent != null)
                    {
                        UpdateAlgoAttentionEvent(int.Parse(value[1]));
                    }
                    break;
                    
                case "meditation":
                    if (UpdateAlgoMeditationEvent != null)
                    {
                        UpdateAlgoMeditationEvent(int.Parse(value[1]));
                    }
                    break;
                    
                case "delta":
                    if (UpdateAlgoDeltaEvent != null)
                    {
                        UpdateAlgoDeltaEvent(float.Parse(value[1]));
                    }
                    break;
            
                case "theta":
                    if (UpdateAlgoThetaEvent != null)
                    {
                        UpdateAlgoThetaEvent(float.Parse(value[1]));
                    }
                    break;
                    
                case "alpha":
                    if (UpdateAlgoAlphaEvent != null)
                    {
                        UpdateAlgoAlphaEvent(float.Parse(value[1]));
                    }
                    break;
                    
                case "beta":
                    if (UpdateAlgoBetaEvent != null)
                    {
                        UpdateAlgoBetaEvent(float.Parse(value[1]));
                    }
                    break
                    
                case "gamma":
                    if (UpdateAlgoGammaEvent != null)
                    {
                        UpdateAlgoGammaEvent(float.Parse(value[1]));
                    }
                    break;
            }
        }
    }

    void OnApplicationQuit()
    {
        checkUpdate = false;
        UnityThinkGear.StopStream();
        UnityThinkGear.Close();	
    }
}
