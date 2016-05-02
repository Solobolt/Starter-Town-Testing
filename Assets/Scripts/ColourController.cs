using UnityEngine;
using System.Collections;

public class ColourController : MonoBehaviour {

    //Holds the renderer of the mesh that should be changed
    public Renderer rend;

    //Determains wether or not the slime fades / gains color to a point. 
    [Tooltip("Ticking this box will stop the slime for slowly returning to a defualt color.")]
    public bool isPlayer = false;
    public float sprintFade = 30.0f;

    //A value to keep track of how saturated the slime is
    private float totalColor;

    //Hold the defualt colors for the slime
    [Header("Defualt Colors")]
    [Range(0.0f, 1.0f)]
    public float defualtRed = 0.5f;

    [Range(0.0f, 1.0f)]
    public float defualtGreen = 0.0f;

    [Range(0.0f, 1.0f)]
    public float defualtBlue = 0.5f;

    [Range(0.01f, 30.0f)]
    public float FadeTime = 20.0f;

    [Space(10)]
    [Header("Current Colors")]
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

    private float blackValue = 0.01f;
    private float whiteValue = 0.01f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //If the slime is not the player then run fade to default
        if(isPlayer == false)
        {
            FadeToDefault();
        }
        else
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                SprintFade();
            }
        }

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
        if (redValue >= 1)
        {
            redValue = 1;
        }
        else
            redValue += otherSlime.redValue;
        if (blueValue >= 1)
        {
            blueValue = 1;
        }
        else
            blueValue += otherSlime.blueValue;

        if (greenValue >= 1)
        {
            greenValue = 1;
        }
        else
            greenValue += otherSlime.greenValue;

        //Replaces the other slimes color
        otherSlime.redValue = 0.0f;
        otherSlime.blueValue = 0.0f;
        otherSlime.greenValue = 0.0f;
    }

    //Changes the colour to a specific value
    public void ChangeColor(float TempRedValue,float TempGreenValue,float TempBlueValue)
    {
        redValue = TempRedValue;
        greenValue = TempGreenValue;
        blueValue = TempBlueValue;

        SetColors();
    }
    
    //Slowly fades the players color if they are sprinting
    public void SprintFade()
    {
        redValue -= 1 / sprintFade * Time.deltaTime;
        blueValue -= 1 / sprintFade * Time.deltaTime;
        greenValue -= 1 / sprintFade * Time.deltaTime;

        //Takes the other slimes color
        if (redValue <= 0)
        {
            redValue = 0;
        }

        if (blueValue <= 0)
        {
            blueValue = 0;
        }

        if (greenValue <= 0)
        {
            greenValue = 0;
        }

        SetColors();
    }

    //Slowly changes the color to the defualt color
    void FadeToDefault()
    {

        //Fades red value
        if(redValue > defualtRed)
        {
            redValue -= 1 / FadeTime * Time.deltaTime;
            if(redValue < defualtRed)
            {
                redValue = defualtRed;
            }
        }

        if (redValue < defualtRed)
        {
            redValue += 1 / FadeTime * Time.deltaTime;
            if (redValue > defualtRed)
            {
                redValue = defualtRed;
            }

        }

        //Fades blue value
        if (blueValue > defualtBlue)
        {
            blueValue -= 1 / FadeTime * Time.deltaTime;
            if (blueValue < defualtBlue)
            {
                blueValue = defualtBlue;
            }
        }

        if (blueValue < defualtBlue)
        {
            blueValue += 1 / FadeTime * Time.deltaTime;
            if (blueValue > defualtBlue)
            {
                blueValue = defualtBlue;
            }

        }

        //Fades Green value
        if(greenValue > defualtGreen)
        {
            greenValue -= 1 / FadeTime * Time.deltaTime;
            if(greenValue < defualtGreen)
            {
                greenValue = defualtGreen;
            }
        }

        if (greenValue < defualtGreen)
        {
            greenValue += 1 / FadeTime * Time.deltaTime;
            if (greenValue > defualtGreen)
            {
                greenValue = defualtGreen;
            }

        }

        /*
        Mathf.Lerp(redValue,defualtRed,FadeTime);
        Mathf.Lerp(greenValue,defualtGreen,FadeTime);
        Mathf.Lerp(blueValue,defualtBlue,FadeTime);
        */

        SetColors();
    }

    //Slowly Mixes the two limes colors
    public void MixColors(ColourController otherSlime)
    {
        //Stores the players color
        float tempRed = redValue;
        float tempBlue = blueValue;
        float tempGreen = greenValue;

        //Stores the other slimes color
        float tempSlimeRed = otherSlime.redValue;
        float tempSlimeGreen = otherSlime.greenValue;
        float tempSlimeBlue = otherSlime.blueValue;

        //Mixes the Players Colors towards the other slime
        Mathf.Lerp(redValue, tempSlimeRed, 2.0f * Time.deltaTime);
        Mathf.Lerp(greenValue, tempSlimeGreen, 2.0f * Time.deltaTime);
        Mathf.Lerp(blueValue, tempSlimeBlue, 2.0f * Time.deltaTime);

        //Mixes the other Slimes Colors towards the players
        Mathf.Lerp(otherSlime.redValue, tempRed, 2.0f * Time.deltaTime);
        Mathf.Lerp(otherSlime.greenValue, tempGreen, 2.0f * Time.deltaTime);
        Mathf.Lerp(otherSlime.blueValue, tempBlue, 2.0f * Time.deltaTime);

        //Make the color change
        SetColors();

        //Sets the color on the other slime too
        otherSlime.SetColors();
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
    public void CleanSlime()
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
