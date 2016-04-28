using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;

public class UIManager : MonoBehaviour {

    //Holds Paused screen Objects 
    public GameObject pausedScreen;
    private GameObject currentPause;
    private bool isPaused = false;

    //Holds Collectables Objects
    public GameObject collectablesScreen;
    private bool showCollectables = false;

    public GameObject huggingGUI;
    public FirstPersonController FPC;

	// Use this for initialization
	void Start () {
        collectablesScreen.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	if(Input.GetKeyDown("escape"))
        {
            TogglePause();
        }
	}

    //Exits the game
    public void QuitGame()
    {
        Application.Quit();
    }

    //Starts the game
    public void StartGame()
    {
        Application.LoadLevel(0);
    }

    // Swaps the paused bool
    public void TogglePause()
    {
        if(isPaused == true)
        {
            isPaused = false;
            Time.timeScale = 1;
            FPC.enabled = true;

            //Hide UI
            Destroy(currentPause.gameObject);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0;
            collectablesScreen.gameObject.SetActive(false);
            //Show UI
            currentPause = Instantiate(pausedScreen) as GameObject;
            FPC.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }
    
    //Enables and dissables the hugging Graphics
    public void ToggleHuggingGUI(bool isHugging)
    {
        if(isHugging == true)
        {
            //Show GUI
            huggingGUI.SetActive(true);
        }
        else
        {
            //Hide GUI
            huggingGUI.SetActive(false);
        }
    }

    //Toggles a bool
    public void ToggleVariable(bool b)
    {
        if(b == true)
            b = false;
        else
            b = true;
    }

    //Toggles the collectable screen
    public void ToggleCollectablesScreen()
    {
        if (showCollectables == true)
        {
            showCollectables = false;
            //Hide UI
            if(collectablesScreen.gameObject.activeInHierarchy)
            {
                collectablesScreen.gameObject.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else
        {
            showCollectables = true;
            //Show UI
            if (!(collectablesScreen.gameObject.activeInHierarchy))
            {
                collectablesScreen.gameObject.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

    }
}
