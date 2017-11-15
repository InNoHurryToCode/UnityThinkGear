using UnityEngine;
using System.Collections;

public class ThinkGearController : MonoBehaviour {
	
	public delegate void UpdateIntValueDelegate(int value);
	public delegate void UpdateFloatValueDelegate(float value);
	public delegate void UpdateStringValueDelegate(string value);

#if UNITY_ANDROID
    public event UpdateStringValueDelegate UpdateConnectStateEvent;
#endif
	
	public event UpdateIntValueDelegate UpdatePoorSignalEvent;
	public event UpdateIntValueDelegate UpdateAttentionEvent;
	public event UpdateIntValueDelegate UpdateMeditationEvent;
	public event UpdateIntValueDelegate UpdateRawdataEvent;
	public event UpdateIntValueDelegate UpdateBlinkEvent;
	
	public event UpdateFloatValueDelegate UpdateDeltaEvent;
	public event UpdateFloatValueDelegate UpdateThetaEvent;
	public event UpdateFloatValueDelegate UpdateLowAlphaEvent;
	public event UpdateFloatValueDelegate UpdateHighAlphaEvent;
	public event UpdateFloatValueDelegate UpdateLowBetaEvent;
	public event UpdateFloatValueDelegate UpdateHighBetaEvent;
	public event UpdateFloatValueDelegate UpdateLowGammaEvent;
	public event UpdateFloatValueDelegate UpdateHighGammaEvent;

#if UNITY_IOS
    public event UpdateStringValueDelegate UpdateDeviceInfoEvent;
#endif

    public event UpdateIntValueDelegate Algo_UpdateAttentionEvent;
    public event UpdateIntValueDelegate Algo_UpdateMeditationEvent;
    public event UpdateFloatValueDelegate Algo_UpdateDeltaEvent;
    public event UpdateFloatValueDelegate Algo_UpdateThetaEvent;
    public event UpdateFloatValueDelegate Algo_UpdateAlphaEvent;
    public event UpdateFloatValueDelegate Algo_UpdateBetaEvent;
    public event UpdateFloatValueDelegate Algo_UpdateGammaEvent;


    private bool sendRawEnable = false;
	private bool sendEEGEnable = false;
	private bool sendESenseEnable = true;
	private bool sendBlinkEnable = true;

    private bool checkUpdate = true;

    void Awake(){
		UnityThinkGear.Init(true);
        StartCoroutine(CheckUpdateCoroutine());
    }
	// Use this for initialization
	void Start () {
	
		sendRawEnable = UnityThinkGear.GetSendRawEnable();
		sendEEGEnable = UnityThinkGear.GetSendEEGEnable();
		sendESenseEnable = UnityThinkGear.GetSendESenseEnable();
		sendBlinkEnable = UnityThinkGear.GetSendBlinkEnable();
	}

    IEnumerator CheckUpdateCoroutine()
    {
        while (checkUpdate)
        {
            if (!sendRawEnable && (UpdateRawdataEvent != null))
            {
                sendRawEnable = true;
                UnityThinkGear.SetSendRawEnable(sendRawEnable);
            }

            if (sendRawEnable && UpdateRawdataEvent == null)
            {
                sendRawEnable = false;
                UnityThinkGear.SetSendRawEnable(sendRawEnable);
            }

            if (!sendEEGEnable &&
            (UpdateDeltaEvent != null || UpdateThetaEvent != null ||
            UpdateLowAlphaEvent != null || UpdateLowBetaEvent != null ||
            UpdateLowGammaEvent != null || UpdateHighAlphaEvent != null ||
            UpdateHighBetaEvent != null || UpdateHighGammaEvent != null))
            {
                sendEEGEnable = true;
                UnityThinkGear.SetSendEEGEnable(sendEEGEnable);
            }

            if (sendEEGEnable &&
            UpdateDeltaEvent == null && UpdateThetaEvent == null &&
            UpdateLowAlphaEvent == null && UpdateLowBetaEvent == null &&
            UpdateLowGammaEvent == null && UpdateHighAlphaEvent == null &&
            UpdateHighBetaEvent == null && UpdateHighGammaEvent == null)
            {
                sendEEGEnable = false;
                UnityThinkGear.SetSendEEGEnable(sendEEGEnable);
            }

            if (!sendESenseEnable && (UpdateAttentionEvent != null || UpdateMeditationEvent != null))
            {
                sendESenseEnable = true;
                UnityThinkGear.SetSendESenseEnable(sendESenseEnable);
            }

            if (sendESenseEnable && UpdateAttentionEvent == null && UpdateMeditationEvent == null)
            {
                sendESenseEnable = false;
                UnityThinkGear.SetSendESenseEnable(sendESenseEnable);
            }

            if (!sendBlinkEnable && (UpdateBlinkEvent != null))
            {
                sendBlinkEnable = true;
                UnityThinkGear.SetSendBlinkEnable(sendBlinkEnable);
            }

            if (sendBlinkEnable && UpdateBlinkEvent == null)
            {
                sendBlinkEnable = false;
                UnityThinkGear.SetSendBlinkEnable(sendBlinkEnable);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    // Update is called once per frame
    void Update () {
	
		
	}
	
	void receiveRawdata(string value){
		if(UpdateRawdataEvent != null){
			UpdateRawdataEvent(int.Parse(value));
		}
	}
	
	void receiveBlink(string value){
		if(UpdateBlinkEvent != null){
			UpdateBlinkEvent(int.Parse(value));
		}
	}
	
	
	void receivePoorSignal(string value){
		if(UpdatePoorSignalEvent != null){
			UpdatePoorSignalEvent(int.Parse(value));
		}
	}
	void receiveAttention(string value){
		if(UpdateAttentionEvent != null){
			UpdateAttentionEvent(int.Parse(value));
		}
	}
	void receiveMeditation(string value){
		if(UpdateMeditationEvent != null){
			UpdateMeditationEvent(int.Parse(value));
		}
	}
	void receiveDelta(string value){
		if(UpdateDeltaEvent != null){
			UpdateDeltaEvent(float.Parse(value));
		}
	}
	void receiveTheta(string value){
		if(UpdateThetaEvent != null){
			UpdateThetaEvent(float.Parse(value));
		}
	}
	void receiveLowAlpha(string value){
		if(UpdateLowAlphaEvent != null){
			UpdateLowAlphaEvent(float.Parse(value));
		}
	}
	void receiveHighAlpha(string value){
		if(UpdateHighAlphaEvent != null){
			UpdateHighAlphaEvent(float.Parse(value));
		}
	}
	void receiveLowBeta(string value){
		if(UpdateLowBetaEvent != null){
			UpdateLowBetaEvent(float.Parse(value));
		}
	}
	void receiveHighBeta(string value){
		if(UpdateHighBetaEvent != null){
			UpdateHighBetaEvent(float.Parse(value));
		}
	}
	void receiveLowGamma(string value){
		if(UpdateLowGammaEvent != null){
			UpdateLowGammaEvent(float.Parse(value));
		}
	}
	void receiveHighGamma(string value){
		if(UpdateHighGammaEvent != null){
			UpdateHighGammaEvent(float.Parse(value));
		}
	}

#if UNITY_IOS
    void receiveDeviceInfo (string deviceInfo){

		print ("!!! Unity deviceInfo :"+deviceInfo);
		if (UpdateDeviceInfoEvent != null) {
			UpdateDeviceInfoEvent (deviceInfo);
		}

	}
#endif

    void receiveEEGAlgorithmValue(string keyValue)
    {
        string[] strs = keyValue.Split(':');
        if(strs != null && strs.Length >= 2)
        {
            if (strs[0].Equals("attention"))
            {
                if(Algo_UpdateAttentionEvent != null)
                {
                    Algo_UpdateAttentionEvent(int.Parse(strs[1]));
                }
            }else if (strs[0].Equals("meditation"))
            {
                if (Algo_UpdateMeditationEvent != null)
                {
                    Algo_UpdateMeditationEvent(int.Parse(strs[1]));
                }
            }
            else if (strs[0].Equals("delta"))
            {
                if (Algo_UpdateDeltaEvent != null)
                {
                    Algo_UpdateDeltaEvent(float.Parse(strs[1]));
                }
            }
            else if (strs[0].Equals("theta"))
            {
                if (Algo_UpdateThetaEvent != null)
                {
                    Algo_UpdateThetaEvent(float.Parse(strs[1]));
                }
            }
            else if (strs[0].Equals("alpha"))
            {
                if (Algo_UpdateAlphaEvent != null)
                {
                    Algo_UpdateAlphaEvent(float.Parse(strs[1]));
                }
            }
            else if (strs[0].Equals("beta"))
            {
                if (Algo_UpdateBetaEvent != null)
                {
                    Algo_UpdateBetaEvent(float.Parse(strs[1]));
                }
            }
            else if (strs[0].Equals("gamma"))
            {
                if (Algo_UpdateGammaEvent != null)
                {
                    Algo_UpdateGammaEvent(float.Parse(strs[1]));
                }
            }
        }
    }


    //====================

#if UNITY_ANDROID
    /*
	   receive "idle" : BT is null or connect status has not been updated
	   receive "connecting": Android device is connecting mindwave headset.
	   receive "connected" : Android device can read data from mindwave headset.
	   receive "not found" : paired mindwave headset is off or not in searchable area.
	   receive "not paired": there are not mindwave headset paired with Android device.
	   receive "disconnected":mindwave headset is off or out of searchable area of Android device.
	   receive "low battery" :mindwave headset's battery dose not have power.
	   receive "bluetooth error" : Android device is not support BT.
    */
    void receiveConnectState1(string value){
		if(UpdateConnectStateEvent != null){
			UpdateConnectStateEvent(value);
		}
	}
#endif	
	//====================


	void OnApplicationQuit(){
        checkUpdate = false;
        UnityThinkGear.StopStream();
		UnityThinkGear.Close();
		
	}



}
