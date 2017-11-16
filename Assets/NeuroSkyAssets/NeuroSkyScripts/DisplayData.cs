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
    private int poorSignal;
    private int attention;
    private int meditation;
    private int raw;
    private int blink;
    private float delta;
    private float theta;
    private float lowAlpha;
    private float highAlpha;
    private float lowBeta;
    private float highBeta;
    private float lowGamma;
    private float highGamma;
    private int algoAttention;
    private int algoMeditation;
    private float algoDelta;
    private float algoTheta;
    private float algoAlpha;
    private float algoBeta;
    private float algoGamma;

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
            Debug.LogError("No canvas text boxes set, please attach them");
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
        UnityThinkGear.Init(true);
    }

    public void Connect()
    {
        Debug.Log("Connect Button Click");
        UnityThinkGear.StartStream();
    }

    public void Quit()
    {
        Debug.Log("Quit Button Click");
        Application.Quit();
    }
}