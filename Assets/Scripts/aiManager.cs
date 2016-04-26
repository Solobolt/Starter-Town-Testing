using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class aiManager : MonoBehaviour {

    public static aiManager instance = null;

    private GameObject player;

    private GameObject[] enemies;

    // Use this for initialization
    void Start ()
    {
        List<GameObject> enemies = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {
	
	}

    void ListAllSlimes()
    {
        

    }

    public void IsNight()
    {

    }

    



}
