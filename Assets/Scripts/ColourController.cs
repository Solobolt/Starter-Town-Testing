using UnityEngine;
using System.Collections;

public class ColourController : MonoBehaviour {

    //Holds the renderer of the mesh that should be changed
    public Renderer rend;

    //A value to keep track of how saturated the slime is
    private float totalColor;

    //Holds color values of the slime
    [Range(0.0f, 1.0f)]
    public float redValue = 0.0f;
    private float redRatios = 0.0f;

    [Range(0.0f, 1.0f)]
    public float greenValue = 0.0f;
    private float greenRatios = 0.0f;

    [Range(0.0f, 1.0f)]
    public float blueValue = 0.0f;
    private float blueRatios = 0.0f;

    [Space(10)]
    private float blackValue = 0.01f;
    private float whiteValue = 0.01f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //Is affective in update for testing but should be placed in hugging scripts later to lower the computing power.
        SetColors();
    }

    //changes the color of public renderer
    private void SetColors()
    {
        //Finds the total of the color values 
        totalColor = redValue + blueValue + greenValue;

        //Breaks the values up into a ratio to be displayed
        redRatios = GetRatio(redValue);
        blueRatios = GetRatio(blueValue);
        greenRatios = GetRatio(greenValue);

        //Renders the 3 main colors
        rend.material.color = Color.Lerp(rend.material.color, Color.red, redRatios);
        rend.material.color = Color.Lerp(rend.material.color, Color.blue, blueRatios);
        rend.material.color = Color.Lerp(rend.material.color, Color.green, greenRatios);
        
        //Renders white and black over the colors if the is too little or too much color
        whiteValue = Mathf.Pow((1 - (totalColor)), 3);
        rend.material.color = Color.Lerp(rend.material.color, Color.white, whiteValue);

        blackValue = Mathf.Pow((totalColor / 3), 3);
        rend.material.color = Color.Lerp(rend.material.color, Color.black, blackValue);
    }

    //Takes the other slimes colors and leaves is white
    public void StealColors(ColourController otherSlime)
    {
        //Takes the other slimes color
        redValue = otherSlime.redValue;
        blueValue = otherSlime.blueValue;
        greenValue = otherSlime.greenValue;

        //Replaces the other slimes color
        otherSlime.redValue = 0.0f;
        otherSlime.blueValue = 0.0f;
        otherSlime.greenValue = 0.0f;
    }

    //Swap Colors
    public void SwapColors(ColourController otherSlime)
    {
        //Stores the players color
        float tempRed = redValue;
        float tempBlue = blueValue;
        float tempGreen = greenValue;

        //Takes the other slimes color
        redValue = otherSlime.redValue;
        blueValue = otherSlime.blueValue;
        greenValue = otherSlime.greenValue;

        //Replaces the other slimes color
        otherSlime.redValue = tempRed;
        otherSlime.blueValue = tempBlue;
        otherSlime.greenValue = tempGreen;

        //Make the color change
        SetColors();
    }

    //Removes all color from the slime and set it to white
    void CleanSlime()
    {
        redValue = 0.0f;
        greenValue = 0.0f;
        blueValue = 0.0f;

        SetColors();
    }

    //Gets the color ratio if there is one
    float GetRatio(float colorValue)
    {
        if(totalColor <= 0.0f)
        {
            return 0.0f;
        }
        return (1 / (redValue + blueValue + greenValue)) * colorValue;
    }
}
