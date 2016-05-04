using UnityEngine;
using System.Collections;
using System.Linq;

public class aiSlime : MonoBehaviour {

	System.Collections.Generic.List<double> a = new System.Collections.Generic.List<double> ();
	public float idleSpeed = 2f;
	public float chaseSpeed;
	public float idleWaitTime;
	public Transform[] idleWayPoints;

	private NavMeshAgent nav;
	public aiManager AIManager;
	public GameObject player;

	public float failTime = 3;
	public float failTimer;

	public float idleTimer;
	private int wayPointIndex;

	ColourController slimeColour;
	public Hugging hugging;

	public bool IsKing = false;
	private bool HasReturned = false;

	public Transform wayPoint;

	public Object waypointTemp;

	public bool failed = false;




	void Awake()
	{
		nav = GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag("Player");
	}


	// Use this for initialization
	void Start () {
		slimeColour = gameObject.GetComponent<ColourController>();
		float average = (slimeColour.blueValue + slimeColour.redValue + slimeColour.greenValue) / 3 ;
		chaseSpeed = average;

		wayPointIndex = Random.Range (0, 3);

		idleWayPoints[0] = (Transform)Instantiate (wayPoint, new Vector3 (transform.position.x + Random.Range(8,10), transform.position.y, transform.position.z), Quaternion.identity);
		idleWayPoints[1] = (Transform)Instantiate (wayPoint, new Vector3 (transform.position.x - 10, transform.position.y, transform.position.z + 10), Quaternion.identity);
		idleWayPoints[2] = (Transform)Instantiate (wayPoint, new Vector3 (transform.position.x - 10, transform.position.y, transform.position.z - 10), Quaternion.identity);



	}
	
	// Update is called once per frame
	void Update () 
	{
		idleWaitTime = Random.Range (0.1f, 5.9f);
		print (failed);

		if (failed) 
		{
			failTimer += Time.deltaTime;
			if (failTimer >= failTime) 
			{
				failed = false;
				failTime = 3;
			}
		}
		
//		if (hugging.IsHugging != true) {
			nav.Resume ();

			if (slimeColour.defualtRed < .1f && slimeColour.defualtBlue < .1f && slimeColour.defualtGreen < .1f && !IsKing) {
				float distance = Vector3.Distance (player.transform.position, transform.position);

				float average = (player.GetComponent<ColourController> ().blueValue + player.GetComponent<ColourController> ().redValue + player.GetComponent<ColourController> ().greenValue) / 3;

				if (distance <= 30 && average > .5f && !failed)
					Faded ();
				else
					Idle ();
			}

			if (slimeColour.defualtRed > .1f || slimeColour.defualtBlue > .1f || slimeColour.defualtGreen > .1f && slimeColour.defualtRed < 1f && slimeColour.defualtBlue < 1f && slimeColour.defualtGreen < 1f && !IsKing) {
				if (HasReturned)
					Idle ();
				else
					Theif ();			
			}
		

			if (IsKing) {
				King ();
			}	
		//} else
		//	nav.Stop();



	}

	void Faded()
	{
		nav.destination = player.transform.position;
		nav.speed = chaseSpeed * 100;

		if (nav.remainingDistance <= nav.stoppingDistance) 
		{
			print ("inside");
			nav.Stop ();
			hugging.IsHugging = true;
			hugging.gettingHugged = true;
		}
			
		
	}

	void Theif()
	{
		nav.speed = chaseSpeed;
		nav.destination = AIManager.SlimeNearSlime (this.gameObject);

		if (nav.remainingDistance <= nav.stoppingDistance) 
		{
			nav.Stop ();
			HasReturned = true;
		}

	}

	void Idle()
	{
		nav.speed = idleSpeed;

		nav.destination = idleWayPoints [wayPointIndex].position;

		if (nav.remainingDistance <= nav.stoppingDistance) {

			idleTimer += Time.deltaTime;

			if (idleTimer >= idleWaitTime) {
			

				if (wayPointIndex == idleWayPoints.Length - 1)
					wayPointIndex = 0;
				else
					wayPointIndex++;

				idleTimer = 0f;
			}
		} else
			idleTimer = 0f;


	}
		

	void King()
	{
		
	}
}
