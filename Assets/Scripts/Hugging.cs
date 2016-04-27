using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hugging : MonoBehaviour {

    public AnimationCurve SlimeFrequencyAndAmplitude;
    public Slider SlimeIcon;




    // Use this for initialization
    void Start ()
    {
        SlimeFrequencyAndAmplitude = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        SlimeFrequencyAndAmplitude.preWrapMode = WrapMode.Loop;
        SlimeFrequencyAndAmplitude.postWrapMode = WrapMode.Loop;

    }
	
	// Update is called once per frame
	void Update ()
    {
              SlimeIcon.value = SlimeFrequencyAndAmplitude.Evaluate(Time.time);
    }
}
