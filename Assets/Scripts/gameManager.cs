using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour {

    //This value is equal to seconds. 
    public float timer;

    //allows other scripts to call functions.
    public static gameManager instance = null;

    //checks if the player has won.
    bool HasWon = false;

    //Holds the value of how many rich slimes have been collected.
    int RainBowProgress;

    //Holds the value of the missing rich slimes.
    int missingColours = 2;

    //Pauses the day/night cycle.
    bool Pause = false;

    //How many rare slimes are avalible to be collected, excluding the glowing slimes.
    int numberOfRareSlimes = 6;

    //An array holding the colours of the rich slimes.
    public Color[] richColoursCollected;

    //An array holding the names of the rare slimes collected
    public string[] rareSlimesCollected;

    //The number of glowing slimes collected.
    public int glowingSlimesCollected;

    aiManager AIManager;

    bool night = false;

    // Use this for initialization
    void Start () {
        //This time is equal to 2 minutes. This is how long a full day night cycle will take.
        timer = 120;

    //Set the size of these arrays.
    rareSlimesCollected = new string[numberOfRareSlimes];
    richColoursCollected = new Color[missingColours];
    }

    //sets up the instance
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update ()
    {
        if(transform.rotation.x <= 90 && transform.rotation.x >= 89)
        {
            night = true;
            if(night && transform.rotation.x <= 89)
            {
                night = false;
                AIManager.IsNight();
            }
        }
        //Checks if the day/night cycle is paused.
        if (!Pause)
        {
            //Rotates to the right. To do one full rotation it takes the timer time.
            transform.RotateAround(Vector3.zero, Vector3.right, (360f / timer) * Time.deltaTime);
            
        }
	}

    //When called checks for a name. This function controlls the rare collectables data.
    public void GetCollectable(string Name)
    {
        //checks if the collected object is glowing.
        if(Name == "Glowing")
        {
            //Adds 1 to this int.
            glowingSlimesCollected++;
        }
        else
            //If the collected object isnt a glowing slime, it checks for a empty position in the array and adds it.
            for (int i = 0; i < numberOfRareSlimes; i++)
            {
                if(rareSlimesCollected[i] == null)
                {
                    rareSlimesCollected[i] = Name;
                    return;
                }
            }
    }

    //When called checks for a colour. This function controlls the rich slime data.
    public void RainbowGenProgress(Color ColourType)
    {
        //adds the rich slime into the array and keeps it's colour.
        richColoursCollected[RainBowProgress] = ColourType;
        //updates the amount of rich slimes collected.
        RainBowProgress++;
        //checks if all the rich slimes have been collected.
        if(RainBowProgress >= missingColours)
        {
            //tells the game it has won.
            HasWon = true;
        }
        
    }
}
