using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Hugging : MonoBehaviour {

    private UIManager uiManager;

    public AnimationCurve SlimeFrequencyAndAmplitude;
    public Slider SlimeBar;
    public Slider HugBar;
    public Slider ColourTransfered;

    public float maxAmplitude;
    public float minAmplitude;
    public float maxBarSpeed;
    public float minBarSpeed;
    public float hugDecreaseValue;
    public float hugMinValue;
    public float hugMaxValue;
    public float transferRate;
    public int frequency;

    public bool IsPlayer;
    private bool IsHugging;
    private bool StartedHugging;
    FirstPersonController FPSController;


    // Use this for initialization
    void Start ()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        FPSController = this.gameObject.GetComponent<FirstPersonController>();
        frequency = 4;
        maxAmplitude = .6f;
        minAmplitude = .3f;
        maxBarSpeed = -5;
        minBarSpeed = 1;
        hugDecreaseValue = 0.02f;
        transferRate = 0.01f;
        IsHugging = false;
        StartedHugging = false;
        ColourTransfered.value = .2f;
        

        for (int i = 0; i < frequency; i++)
        {
            SlimeFrequencyAndAmplitude.AddKey((Random.Range(minBarSpeed,maxBarSpeed)), (Random.Range(minAmplitude, maxAmplitude)));
        }

            //  SlimeFrequencyAndAmplitude = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
            SlimeFrequencyAndAmplitude.preWrapMode = WrapMode.PingPong;
        SlimeFrequencyAndAmplitude.postWrapMode = WrapMode.PingPong;

        SlimeFrequencyAndAmplitude.RemoveKey(0);
        SlimeFrequencyAndAmplitude.RemoveKey(frequency);

        //SlimeFrequencyAndAmplitude.AddKey(.5f, -2.5f);

    }

    // Update is called once per frame
    void Update()
    {
        hugMaxValue = HugBar.value + .1f;
        hugMinValue = hugMaxValue - .2f;

        if (IsHugging == true)
        {

           

            if (Input.GetKey(KeyCode.E))
            {

                StartedHugging = true;
                HugBar.value += hugDecreaseValue;
            }

            if (StartedHugging == true)
            {
                HugBar.value -= hugDecreaseValue / 4;

                SlimeBar.value = SlimeFrequencyAndAmplitude.Evaluate(Time.time);

                if (SlimeBar.value <= hugMaxValue && SlimeBar.value >= hugMinValue)
                {
                    ColourTransfered.value += transferRate;
                }
                else
                    ColourTransfered.value -= transferRate / 4;

                if (ColourTransfered.value <= 0)
                {
                    for (int i = 0; i < SlimeFrequencyAndAmplitude.length; i++)
                    {
                        SlimeFrequencyAndAmplitude.RemoveKey(0);
                    }

                    for (int i = 0; i < frequency; i++)
                    {
                        SlimeFrequencyAndAmplitude.AddKey((Random.Range(minBarSpeed, maxBarSpeed)), (Random.Range(minAmplitude, maxAmplitude)));
                    }

                    if (FPSController.enabled == false)
                        FPSController.enabled = true;
                    StartedHugging = false;
                    IsHugging = false;

                    uiManager.ToggleHuggingGUI(false);

                    HugBar.value = 0;
                    SlimeBar.value = 0;
                    ColourTransfered.value = .2f;
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                for (int i = 0; i < SlimeFrequencyAndAmplitude.length; i++)
                {
                    SlimeFrequencyAndAmplitude.RemoveKey(0);
                }

                for (int i = 0; i < frequency; i++)
                {
                    SlimeFrequencyAndAmplitude.AddKey((Random.Range(minBarSpeed, maxBarSpeed)), (Random.Range(minAmplitude, maxAmplitude)));
                }

                if (FPSController.enabled == false)
                    FPSController.enabled = true;
                StartedHugging = false;
                IsHugging = false;

                uiManager.ToggleHuggingGUI(false);

                HugBar.value = 0;
                SlimeBar.value = 0;
                ColourTransfered.value = .2f;
            }
        }
    }

    //Detects if the play interacts with another slime
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Slime")
        {
            uiManager.ToggleHuggingGUI(true);
            if (FPSController.enabled == true)
                FPSController.enabled = false;
            IsHugging = true;
        }
    }
}
