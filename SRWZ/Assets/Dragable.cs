using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Dragable : MonoBehaviour
{
	// BiteScript
	BiteScript biteScript;

	public int normalCollisionCount = 1;
	public float moveLimit = .5f;
	public float collisionMoveFactor = .01f;
	public float addHeightWhenClicked = 0.0f;
	public bool freezeRotationOnDrag = true;
	public Camera cam;
	private Rigidbody myRigidbody;
	private Transform myTransform;
	private Vector3 parentPosition;
	private Vector3 housePosition;
	private TrailRenderer trail;
	// length of time to display trail
	// this is really large so it essentially never disappears
	public float trailTime = 10000;
	public float maxOriginDist = 8f;
	private bool canMove = false;
	private float yPos;
	private bool gravitySetting;
	private bool freezeRotationSetting;
	private float sqrMoveLimit;
	private int collisionCount = 0;
	private Transform camTransform;
	
	void Start () 
	{
		// get the BiteScript
		biteScript = this.GetComponent<BiteScript>();

		// get the trail renderer
		trail = this.GetComponent<TrailRenderer>();

		myRigidbody = rigidbody;
		myTransform = transform;
		parentPosition = transform.parent.transform.position;
		housePosition = GameObject.Find("House").transform.position;
		if (!cam)
		{
			cam = Camera.main;
		}
		if (!cam)
		{
			Debug.LogError("Can't find camera tagged MainCamera");
			return;
		}
		camTransform = cam.transform;
		sqrMoveLimit = moveLimit * moveLimit;   // Since we're using sqrMagnitude, which is faster than magnitude
	}
	
	void OnMouseDown () 
	{
		canMove = true;
		myTransform.Translate(Vector3.up*addHeightWhenClicked);
		gravitySetting = myRigidbody.useGravity;
		freezeRotationSetting = myRigidbody.freezeRotation;
		myRigidbody.useGravity = false;
		myRigidbody.freezeRotation = freezeRotationOnDrag;
		yPos = myTransform.position.y;
	}
	
	void OnMouseUp () 
	{
		canMove = false;
		myRigidbody.useGravity = gravitySetting;
		myRigidbody.freezeRotation = freezeRotationSetting;
		if (!myRigidbody.useGravity) 
		{
			Vector3 pos = myTransform.position;
			pos.y = yPos-addHeightWhenClicked;
			myTransform.position = pos;
		}

		GameObject[] zoms = GameObject.FindGameObjectsWithTag("Zombie");
		Debug.Log ("Missed zoms: " + zoms.Length);
		GameObject ZSave = GameObject.FindGameObjectWithTag("ZomSaver");
		ZomSaver scriptZSave = (ZomSaver)ZSave.GetComponent<ZomSaver>();

		if (scriptZSave.savedZombies.Count > 0) {
			GameObject ZSpawn = GameObject.FindGameObjectWithTag("ZomSpawner");
			Spawner scriptZSpawn = (Spawner)ZSpawn.GetComponent<Spawner>();
			scriptZSave.remainingSpawns = 20 - scriptZSpawn.spawned;
			foreach (GameObject z in zoms) {
				if (z.renderer.enabled) {
					scriptZSave.missedZombies.Add(z);
				}
			}
			Application.LoadLevel("SizeScroll");
		}

	}
	
	void OnCollisionEnter () 
	{
		collisionCount++;
	}
	
	void OnCollisionExit () 
	{
		collisionCount--;
	}

	// reset the player ghost trail time
	void ResetTrail()
	{
		trail.time = trailTime;
	}

	void FixedUpdate () 
	{
		// if we can't move, we're out of fuel, & the ghost's position is not near the player's position
		if (!canMove && biteScript.fuelLeft <= 0 && Vector3.Distance(housePosition, myTransform.position) > maxOriginDist)
		{
			biteScript.fuelLeft = biteScript.maxFuel;
			myTransform.position = parentPosition;
			trail.time = 0;
			Invoke("ResetTrail", 1);
			return;
		}

		// if we can't move or there's no fuel left
		if (!canMove || biteScript.fuelLeft <= 0)
		{
			return;
		}
		
		myRigidbody.velocity = Vector3.zero;
		myRigidbody.angularVelocity = Vector3.zero;
		
		Vector3 pos = myTransform.position;
		pos.y = yPos;
		myTransform.position = pos;
		
		Vector3 mousePos = Input.mousePosition;
		Vector3 move = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, camTransform.position.y - myTransform.position.y)) - myTransform.position;
		move.y = 0.0f;
		if (collisionCount > normalCollisionCount)		
		{
			move = move.normalized*collisionMoveFactor;
		}
		else if (move.sqrMagnitude > sqrMoveLimit) 
		{
			move = move.normalized*moveLimit;
		}
		
		myRigidbody.MovePosition(myRigidbody.position + move);
	}
}