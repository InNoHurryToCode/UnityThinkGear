using UnityEngine;
using System.Collections;

public class DisplayData : MonoBehaviour {
	
	public Texture2D[] signalIcons;
	
	private float indexSignalIcons = 1;
	private bool enableAnimation = false;
	private float animationInterval = 0.06f;
	
    ThinkGearController controller;
	
	private int Raw = 0;
	private int PoorSignal = 200;
	private int Attention = 0;
	private int Meditation = 0;
	private int Blink = 0;
	private float Delta = 0.0f;
	private float Theta = 0.0f;
	private float LowAlpha = 0.0f;
	private float HighAlpha = 0.0f;
	private float LowBeta = 0.0f;
	private float HighBeta = 0.0f;
	private float LowGamma = 0.0f;
	private float HighGamma = 0.0f;

    private int Algo_Attention = 0;
    private int Algo_Meditation = 0;
    private float Algo_Delta = 0.0f;
    private float Algo_Theta = 0.0f;
    private float Algo_Alpha = 0.0f;
    private float Algo_Beta = 0.0f;
    private float Algo_Gamma = 0.0f;

#if UNITY_ANDROID
    private string ConnectionState = "";
#endif
#if UNITY_IOS
    private string DeviceInfo = "";
#endif

	// Use this for initialization
	void Start () {
		controller = GameObject.Find("ThinkGear").GetComponent<ThinkGearController>();

		controller.UpdateRawdataEvent += OnUpdateRaw;
		controller.UpdatePoorSignalEvent += OnUpdatePoorSignal;
		controller.UpdateAttentionEvent += OnUpdateAttention;
		controller.UpdateMeditationEvent += OnUpdateMeditation;
		
		controller.UpdateDeltaEvent += OnUpdateDelta;
		controller.UpdateThetaEvent += OnUpdateTheta;

		controller.UpdateHighAlphaEvent += OnUpdateHighAlpha;
		controller.UpdateHighBetaEvent += OnUpdateHighBeta;
		controller.UpdateHighGammaEvent += OnUpdateHighGamma;

		controller.UpdateLowAlphaEvent += OnUpdateLowAlpha;
		controller.UpdateLowBetaEvent += OnUpdateLowBeta;
		controller.UpdateLowGammaEvent += OnUpdateLowGamma;

		controller.UpdateBlinkEvent += OnUpdateBlink;

#if UNITY_ANDROID
        controller.UpdateConnectStateEvent += OnUpdateConnectionState;
#endif

#if UNITY_IOS
        controller.UpdateDeviceInfoEvent += OnUpdateDeviceInfo;
#endif

        controller.Algo_UpdateAttentionEvent += OnAlgo_UpdateAttentionEvent;
        controller.Algo_UpdateMeditationEvent += OnAlgo_UpdateMeditationEvent;
        controller.Algo_UpdateDeltaEvent += OnAlgo_UpdateDeltaEvent;
        controller.Algo_UpdateThetaEvent += OnAlgo_UpdateThetaEvent;
        controller.Algo_UpdateAlphaEvent += OnAlgo_UpdateAlphaEvent;
        controller.Algo_UpdateBetaEvent += OnAlgo_UpdateBetaEvent;
        controller.Algo_UpdateGammaEvent += OnAlgo_UpdateGammaEvent;
	}

    void OnAlgo_UpdateAttentionEvent(int value)
    {
        Algo_Attention = value;
    }
    void OnAlgo_UpdateMeditationEvent(int value)
    {
        Algo_Meditation = value;

    }

    void OnAlgo_UpdateDeltaEvent(float value)
    {
        Algo_Delta = value;
    }
    void OnAlgo_UpdateThetaEvent(float value)
    {
        Algo_Theta = value;
    }
    void OnAlgo_UpdateAlphaEvent(float value)
    {
        Algo_Alpha = value;
    }
    void OnAlgo_UpdateBetaEvent(float value)
    {
        Algo_Beta = value;
    }
    void OnAlgo_UpdateGammaEvent(float value)
    {
        Algo_Gamma = value;
    }


    void OnUpdatePoorSignal(int value){
		PoorSignal = value;
		if(value == 0){
      		indexSignalIcons = 0;
			enableAnimation = false;
		}else if(value == 200){
      		indexSignalIcons = 1;
			enableAnimation = false;
		}else if(!enableAnimation){
			indexSignalIcons = 2;
			enableAnimation = true;
		}
	}
	void OnUpdateRaw(int value){
		Raw = value;
	}
	void OnUpdateAttention(int value){
		Attention = value;
	}
	void OnUpdateMeditation(int value){
		Meditation = value;

	}
	void OnUpdateDelta(float value){
		Delta = value;
	}
	void OnUpdateTheta(float value){
		Theta = value;
	}
	void OnUpdateHighAlpha(float value){
		HighAlpha = value;
	}
	void OnUpdateHighBeta(float value){
		HighBeta = value;
	}
	void OnUpdateHighGamma(float value){
		HighGamma = value;
	}
	void OnUpdateLowAlpha(float value){
		LowAlpha = value;
	}
	void OnUpdateLowBeta(float value){
		LowBeta = value;
	}
	void OnUpdateLowGamma(float value){
		LowGamma = value;
	}

	void OnUpdateBlink(int value){
		Blink = value;
	}

#if UNITY_ANDROID
    void OnUpdateConnectionState(string value)
    {
        ConnectionState = value;
    }
#endif

#if UNITY_IOS
    void OnUpdateDeviceInfo(string value){
        //deviceFound deviceInfo = NSF4F1BF;MindWave Mobile;BAFCEB11-2DB6-70B3-B038-B4AD2EFC6309
        // FMGID ; name ; ConnectId
        DeviceInfo = value;
	}
#endif

    /**
	 *when Fixed Timestep == 0.02 
	 **/
    void FixedUpdate(){
		if(enableAnimation){
			if(indexSignalIcons >= 4.8){
				indexSignalIcons = 2;
			}
			indexSignalIcons += animationInterval;
		}
		
	}
	
	void OnGUI(){
		GUILayout.BeginHorizontal();
		GUILayout.Label("Demo App");
		GUILayout.Space(Screen.width-250);
		GUILayout.Label(signalIcons[(int)indexSignalIcons]);
		GUILayout.EndHorizontal();
		
		if(GUI.Button( new Rect(190,20,100,80),"Init")){
			UnityThinkGear.Init(true);
		}
		
		if(GUI.Button(new Rect(190,140,100,80),"Connect")){
			print("Connect Button CLick");
#if UNITY_ANDROID || UNITY_IOS
			UnityThinkGear.StartStream();
#endif

        }

        if (GUI.Button(new Rect(190,250,100,80),"Quit")){
			Application.Quit();
		}

		
		GUILayout.BeginVertical();
		GUILayout.Label("Raw:" + Raw);
		GUILayout.Label("PoorSignal:" + PoorSignal);
		GUILayout.Label("Attention:" + Attention);
		GUILayout.Label("Meditation:" + Meditation);
		GUILayout.Label("Blink:" + Blink);
		GUILayout.Label("Delta:" + Delta);
		GUILayout.Label("Theta:" + Theta);
		GUILayout.Label("LowAlpha:" + LowAlpha);
		GUILayout.Label("HighAlpha:" + HighAlpha);
		GUILayout.Label("LowBeta:" + LowBeta);
		GUILayout.Label("HighBeta:" + HighBeta);
		GUILayout.Label("LowGamma:" + LowGamma);
		GUILayout.Label("HighGamma:" + HighGamma);
        GUILayout.Label("");
        GUILayout.Label("");
        GUILayout.Label("EEG Algorithm output values");
        GUILayout.Label("Attention:" + Algo_Attention);
        GUILayout.Label("Meditation:" + Algo_Meditation);
        GUILayout.Label("Delta:" + Algo_Delta);
        GUILayout.Label("Theta:" + Algo_Theta);
        GUILayout.Label("Alpha:" + Algo_Alpha);
        GUILayout.Label("Beta:" + Algo_Beta);
        GUILayout.Label("Gamma:" + Algo_Gamma);
        GUILayout.Label("");
        GUILayout.Label("");
#if UNITY_ANDROID
        GUILayout.Label("ConnectionState:" + ConnectionState);
#endif
#if UNITY_IOS
        GUILayout.Label("DeviceInfo" + DeviceInfo);
#endif
        GUILayout.EndVertical();
	}	
}
