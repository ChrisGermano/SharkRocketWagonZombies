#pragma strict

public var zombie : GameObject;

public var xPlaces : int[];
public var spawnedZombies : GameObject[];
private var xCount : int;

public var spawnRate : int;
private var spawnTick : int;

public var moveTime : float;
public var moveDist : int;

//keep track of how many zombies have completd level (dead or alive, hehe)
private var completed : int;

function Start () {
	spawnTick = 0;
	xCount = 0;
	spawnedZombies = new GameObject[xPlaces.Length];
}

function Update () {
	spawnTick++;
	if (spawnTick >= spawnRate) {
		spawnTick = 0;
		xCount++;
		var doneCheck : boolean = true;
		var fullZoms : boolean = false;
		for (var i : int = 0; i < xPlaces.Length; i++) {
			if (xCount == xPlaces[i]) {
				
				if (xPlaces[i] != -1) {
					doneCheck = false;
				}
				
				var newX : float = -13+(Random.Range(0,2)*13);
				var newZom : GameObject = GameObject.Instantiate(zombie, new Vector3(newX,-3,0), Quaternion.identity);
				spawnedZombies[i] = newZom;
				if (i == xPlaces.Length-1) {
					fullZoms = true;
				}
				move(newZom);
				xPlaces[i] = -1;
			}
		}
		if (doneCheck && fullZoms) {
			var ateCount : int = 0;
			for (var x : int = 0; x < xPlaces.Length; x++) {
				if (!spawnedZombies[x].renderer.enabled) {
					ateCount++;
					Debug.Log("Total Ate = " + ateCount);
				}
			}
		}
		if(completed >= xPlaces.Length) {
			//Debug.Log("Zombie Results : " + completed + "/" + xPlaces.Length);
			//if we are done, Angus is alive, switch back to stage 1
			Application.LoadLevel("TopDownTest");
		}
	}
}

function move(zombie : GameObject) {
	yield StartCoroutine("moveZom",zombie);
}

function moveZom(zom : GameObject) {
	var timeSpent : float = 0;
	var startPos : Vector3 = zom.transform.position;
	var newPos : Vector3 = startPos;
	newPos.z -= moveDist;
	
	
	while (timeSpent < moveTime) {
		zom.transform.position = Vector3.Lerp(startPos, newPos, (timeSpent/moveTime));
		timeSpent += Time.deltaTime;
		yield;
	}
	//The zombie has reached the end of the level, this accounts for all
	//zombies, including eaten ones because they just become invisible
	//Debug.Log("ZOMBIE PASSED, COMPLETED = " + completed);
	completed++;
}