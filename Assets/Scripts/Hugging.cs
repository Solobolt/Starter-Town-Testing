using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Hugging : MonoBehaviour {

    private UIManager uiManager;
    private ColourController colourController;

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

	GameObject currentSlime;
	
    public bool IsPlayer;
    public bool IsHugging;
	public bool gettingHugged;
    private bool StartedHugging;
    FirstPersonController FPSController;
	public aiSlime AISlime;

    public ColourController TargetSlime;

    // Use this for initialization
    void Start ()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        colourController = this.gameObject.GetComponent<ColourController>();
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

        //SlimeFrequencyAndAmplitude.RemoveKey(0);
        //SlimeFrequencyAndAmplitude.RemoveKey(frequency);

        //SlimeFrequencyAndAmplitude.AddKey(.5f, -2.5f);

    }

    // Update is called once per frame
    void Update()
    {
		RaycastHit hit;
		Vector3 fwd = transform.TransformDirection (Vector3.forward);
		Debug.DrawRay (transform.position, -Vector3.up, Color.blue);
		if (Physics.Raycast (transform.position, fwd,out hit, 1))
		{
			//print ("hit");
			if (hit.transform.tag == "Slime")
			{
				if (gettingHugged) {
					
				}
				ColourController colour = hit.transform.gameObject.GetComponent<ColourController> ();
				if (colour.greenValue >= colour.defualtGreen - .10f && colour.redValue >= colour.defualtRed - .10f && colour.blueValue >= colour.defualtBlue - .10f) {
					TargetSlime = hit.transform.gameObject.GetComponent<ColourController> ();
					uiManager.ToggleHuggingGUI (true);
					if (FPSController.enabled == true)
						FPSController.enabled = false;

					IsHugging = true;
				}
			}
		}
		
        hugMaxValue = HugBar.value + .1f;
        hugMinValue = hugMaxValue - .2f;

        if (IsHugging == true)
        {

           

            if (Input.GetKey(KeyCode.Space))
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
                    for (int i = 0; i < frequency; i++)
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

                if (ColourTransfered.value >= 1)
                {
                    colourController.StealColors(TargetSlime);
					if (gettingHugged) 
					{
						AISlime = currentSlime.GetComponent<aiSlime> ();
//						AISlime.OutCome (true);
					}

                    for (int i = 0; i < frequency; i++)
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
                for (int i = 0; i < frequency; i++)
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
	
	public void GettingHugged(GameObject OtherSlime)
	{
		currentSlime = OtherSlime;
		gettingHugged = true;

	}

    //Detects if the play interacts with another slime
   // void OnControllerColliderHit(ControllerColliderHit hit)
   // {
    //    if (hit.transform.tag == "Slime")
     //   {
     //       TargetSlime = hit.gameObject.GetComponent<ColourController>();
      //      uiManager.ToggleHuggingGUI(true);
      //      if (FPSController.enabled == true)
       //         FPSController.enabled = false;
       //     IsHugging = true;
       // }
    //}
}
