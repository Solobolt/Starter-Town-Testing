using UnityEngine;
using System.Collections;

public class GateController : MonoBehaviour {

    private bool isOpen = false;
    private ColourController colorController;

    //Hold the door object that blocks the players path
    public GameObject door;

    [Space(20)]
    //Gate Requirements
    [Range(0.0f,1.0f)]
    public float redReq = 1.0f;

    [Range(0.0f, 1.0f)]
    public float greenReq = 1.0f;

    [Range(0.0f, 1.0f)]
    public float blueReq = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Opens the gate if the player is the correct Color
    void OnCollisionEnter(Collision coll)
    {
        //Checks that it is the player interacting with the door
        if(coll.gameObject.tag == "Player")
        {
            //Checks wether or not the player has the necicary colors to open tyhe door
            colorController = coll.gameObject.GetComponent<ColourController>();
            if(colorController.redValue >= redReq && colorController.blueValue >= blueReq && colorController.greenValue >= greenReq)
            {
                isOpen = true;
                colorController.CleanSlime();
            }
        }

        //Opens the door
        if(isOpen == true)
        {
            door.gameObject.SetActive(false);
        }
    }
}
