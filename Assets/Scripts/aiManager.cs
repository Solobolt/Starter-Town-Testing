using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class aiManager : MonoBehaviour {

    public static aiManager instance = null;

    private GameObject player;

	public GameObject[] Slimes;

    private GameObject[] enemies;

	float CurrentClosestSlime = 1000;
	public GameObject ClosestSlime;
	public float timer;
	public Hugging hugging;
	
    // Use this for initialization
    void Start ()
    {
		Slimes = GameObject.FindGameObjectsWithTag ("Slime");
        player = GameObject.FindGameObjectWithTag("Player");
		hugging = player.GetComponent<Hugging> ();
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

    public Vector3 SlimeNearSlime(GameObject slime)
	{
		for (int i = 0; i < Slimes.Length; i++)
		{
			
			float slimeDistance = Vector3.Distance (slime.transform.position, Slimes [i].transform.position);

			if (slimeDistance < CurrentClosestSlime) 
			{
				CurrentClosestSlime = slimeDistance;
				ClosestSlime = Slimes [i];
			}
		}
		return(ClosestSlime.gameObject.transform.position);
	}



}
