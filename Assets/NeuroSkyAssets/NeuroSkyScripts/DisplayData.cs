using UnityEngine;

public class DisplayData : MonoBehaviour
{
    private ThinkGearController controller;

    #if UNITY_ANDROID
    private string connectionState = "";
    #elif UNITY_IOS
    private string deviceInfo = "";
    #endif
    private int poorSignal = 200;
    private int attention = 0;
    private int meditation = 0;
    private int raw = 0;
    private int blink = 0;
    private float delta = 0.0f;
    private float theta = 0.0f;
    private float lowAlpha = 0.0f;
    private float highAlpha = 0.0f;
    private float lowBeta = 0.0f;
    private float highBeta = 0.0f;
    private float lowGamma = 0.0f;
	private float highGamma = 0.0f;
    private int algoAttention = 0;
    private int algoMeditation = 0;
    private float algoDelta = 0.0f;
    private float algoTheta = 0.0f;
    private float algoAlpha = 0.0f;
    private float algoBeta = 0.0f;
    private float algoGamma = 0.0f;

    public Texture2D[] SignalIcons;
    private float indexSignalIcons = 1;
    private bool enableAnimation = false;
    private float animationInterval = 0.06f;

    void Start()
    {
        controller = GameObject.Find("ThinkGear").GetComponent<ThinkGearController>();

        #if UNITY_ANDROID
        controller.UpdateConnectStateEvent += OnUpdateConnectionState;
        #elif UNITY_IOS
        controller.UpdateDeviceInfoEvent += OnUpdateDeviceInfo;
        #endif
        controller.UpdatePoorSignalEvent += OnUpdatePoorSignal;
        controller.UpdateAttentionEvent += OnUpdateAttention;
        controller.UpdateMeditationEvent += OnUpdateMeditation;
        controller.UpdateRawdataEvent += OnUpdateRaw;
        controller.UpdateBlinkEvent += OnUpdateBlink;
        controller.UpdateDeltaEvent += OnUpdateDelta;
        controller.UpdateThetaEvent += OnUpdateTheta;
        controller.UpdateLowAlphaEvent += OnUpdateLowAlpha;
        controller.UpdateHighAlphaEvent += OnUpdateHighAlpha;
        controller.UpdateLowBetaEvent += OnUpdateLowBeta;
        controller.UpdateHighBetaEvent += OnUpdateHighBeta;
        controller.UpdateLowGammaEvent += OnUpdateLowGamma;
        controller.UpdateHighGammaEvent += OnUpdateHighGamma;
        controller.UpdateAlgoAttentionEvent += OnUpdateAlgoAttentionEvent;
        controller.UpdateAlgoMeditationEvent += OnUpdateAlgoMeditationEvent;
        controller.UpdateAlgoDeltaEvent += OnUpdateAlgoDeltaEvent;
        controller.UpdateAlgoThetaEvent += OnUpdateAlgoThetaEvent;
        controller.UpdateAlgoAlphaEvent += OnUpdateAlgoAlphaEvent;
        controller.UpdateAlgoBetaEvent += OnUpdateAlgoBetaEvent;
        controller.UpdateAlgoGammaEvent += OnUpdateAlgoGammaEvent;
    }

    #if UNITY_ANDROID
    void OnUpdateConnectionState(string value)
    {
        connectionState = value;
    }
    #elif UNITY_IOS
    void OnUpdateDeviceInfo(string value)
    {
       deviceInfo = value;
    }
    #endif

    void OnUpdatePoorSignal(int value)
    {
        poorSignal = value;

        if (value == 0)
        {
            indexSignalIcons = 0;
            enableAnimation = false;
        }
        else if (value == 200)
        {
            indexSignalIcons = 1;
            enableAnimation = false;
        }
        else if (!enableAnimation)
        {
            indexSignalIcons = 2;
            enableAnimation = true;
        }
    }

    void OnUpdateAttention(int value)
    {
        attention = value;
    }

    void OnUpdateMeditation(int value)
    {
        meditation = value;
    }

    void OnUpdateRaw(int value)
    {
        raw = value;
    }

    void OnUpdateBlink(int value)
    {
        blink = value;
    }

    void OnUpdateDelta(float value)
    {
        delta = value;
    }

    void OnUpdateTheta(float value)
    {
        theta = value;
    }

    void OnUpdateLowAlpha(float value)
    {
        lowAlpha = value;
    }

    void OnUpdateHighAlpha(float value)
    {
        highAlpha = value;
    }

    void OnUpdateLowBeta(float value)
    {
        lowBeta = value;
    }

    void OnUpdateHighBeta(float value)
    {
        highBeta = value;
    }

    void OnUpdateLowGamma(float value)
    {
        lowGamma = value;
    }

    void OnUpdateHighGamma(float value)
    {
        highGamma = value;
    }

    void OnUpdateAlgoAttentionEvent(int value)
    {
        algoAttention = value;
    }

    void OnUpdateAlgoMeditationEvent(int value)
    {
        algoMeditation = value;
    }

    void OnUpdateAlgoDeltaEvent(float value)
    {
        algoDelta = value;
    }

    void OnUpdateAlgoThetaEvent(float value)
    {
        algoTheta = value;
    }

    void OnUpdateAlgoAlphaEvent(float value)
    {
        algoAlpha = value;
    }

    void OnUpdateAlgoBetaEvent(float value)
    {
        algoBeta = value;
    }

    void OnUpdateAlgoGammaEvent(float value)
    {
        algoGamma = value;
    }

    void FixedUpdate()
    {
        if (enableAnimation)
        {
            if (indexSignalIcons >= 4.8)
            {
                indexSignalIcons = 2;
            }

            indexSignalIcons += animationInterval;
        }
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Demo App");
        GUILayout.Space(Screen.width - 250);
        GUILayout.Label(SignalIcons[(int)indexSignalIcons]);
        GUILayout.EndHorizontal();

        if (GUI.Button(new Rect(190, 20, 100, 80), "Init"))
        {
            Debug.Log("Init Button Click");
            UnityThinkGear.Init(true);
        }

        if (GUI.Button(new Rect(190, 140, 100, 80), "Connect"))
        {
            Debug.Log("Connect Button Click");
            #if UNITY_ANDROID || UNITY_IOS
            UnityThinkGear.StartStream();
            #endif
        }

        if (GUI.Button(new Rect(190, 250, 100, 80), "Quit"))
        {
            Application.Quit();
        }

        GUILayout.BeginVertical();
        GUILayout.Label("Raw:" + raw);
        GUILayout.Label("PoorSignal:" + poorSignal);
        GUILayout.Label("Attention:" + attention);
        GUILayout.Label("Meditation:" + meditation);
        GUILayout.Label("Blink:" + blink);
        GUILayout.Label("Delta:" + delta);
        GUILayout.Label("Theta:" + theta);
        GUILayout.Label("LowAlpha:" + lowAlpha);
        GUILayout.Label("HighAlpha:" + highAlpha);
        GUILayout.Label("LowBeta:" + lowBeta);
        GUILayout.Label("HighBeta:" + highBeta);
        GUILayout.Label("LowGamma:" + lowGamma);
        GUILayout.Label("HighGamma:" + highGamma);
        GUILayout.Label("");
        GUILayout.Label("");
        GUILayout.Label("EEG Algorithm output values");
        GUILayout.Label("Attention:" + algoAttention);
        GUILayout.Label("Meditation:" + algoMeditation);
        GUILayout.Label("Delta:" + algoDelta);
        GUILayout.Label("Theta:" + algoTheta);
        GUILayout.Label("Alpha:" + algoAlpha);
        GUILayout.Label("Beta:" + algoBeta);
        GUILayout.Label("Gamma:" + algoGamma);
        GUILayout.Label("");
        GUILayout.Label("");
        #if UNITY_ANDROID
        GUILayout.Label("ConnectionState:" + connectionState);
        #elif UNITY_IOS
        GUILayout.Label("DeviceInfo" + deviceInfo);
        #endif
        GUILayout.EndVertical();
    }	
}