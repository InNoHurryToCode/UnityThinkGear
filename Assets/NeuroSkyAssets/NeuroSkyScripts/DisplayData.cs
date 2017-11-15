using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private Sprite[] signalIcons;
    private float indexSignalIcons = 1;
    private bool enableAnimation = false;
    private float animationInterval = 0.06f;

    [SerializeField] private Image canvasSignalIcon;
    [SerializeField] private Text[] canvasText;

    void Awake()
    {
        if (signalIcons == null)
        {
            Debug.LogError("No signal icons found, please attach them");
        }

        if (canvasText == null)
        {
            Debug.LogError("No textboxes set, please attach them");
        }
    }

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


    void Update()
    {
        if (enableAnimation)
        {
            if (indexSignalIcons >= 4.8)
            {
                indexSignalIcons = 2;
            }

            indexSignalIcons += animationInterval;
        }

        canvasSignalIcon.sprite = signalIcons[(int)indexSignalIcons];

        canvasText[0].text = "PoorSignal: " + poorSignal;
        canvasText[1].text = "Attention: " + attention;
        canvasText[2].text = "Meditation: " + meditation;
        canvasText[3].text = "Raw: " + raw;
        canvasText[4].text = "Blink: " + blink;
        canvasText[5].text = "Delta: " + delta;
        canvasText[6].text = "Theta: " + theta;
        canvasText[7].text = "LowAlpha: " + lowAlpha;
        canvasText[8].text = "HighAlpha: " + highAlpha;
        canvasText[9].text = "LowBeta: " + lowBeta;
        canvasText[10].text = "HighBeta: " + highBeta;
        canvasText[11].text = "LowGamma: " + lowGamma;
        canvasText[12].text = "HighGamma: " + highGamma;
        canvasText[13].text = "AlgoAttention: " + algoAttention;
        canvasText[14].text = "AlgoMeditation: " + algoMeditation;
        canvasText[15].text = "AlgoDelta: " + algoDelta;
        canvasText[16].text = "AlgoTheta: " + algoTheta;
        canvasText[17].text = "AlgoAlpha: " + algoAlpha;
        canvasText[18].text = "AlgoBeta: " + algoBeta;
        canvasText[19].text = "AlgoGamma: " + algoGamma;
        #if UNITY_ANDROID
        canvasText[20].text = "ConnectionState: " + connectionState;
        #elif UNITY_IOS
        textBoxes[20].text = "DeviceInfo: " + deviceInfo;
        #endif
    }

    public void Init()
    {
        Debug.Log("Init Button Click");

        #if UNITY_ANDROID || UNITY_IOS
        UnityThinkGear.Init(true);
        #endif
    }

    public void Connect()
    {
        Debug.Log("Connect Button Click");

        #if UNITY_ANDROID || UNITY_IOS
        UnityThinkGear.StartStream();
        #endif
    }

    public void Quit()
    {
        Debug.Log("Quit Button Click");

        #if UNITY_ANDROID || UNITY_IOS
        Application.Quit();
        #endif
    }
}