using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

	// BiteScript
	BiteScript biteScript;

	// Use this for initialization
	void Start () {
		// get the BiteScript
		biteScript = GameObject.FindGameObjectWithTag("PlayerGhost").GetComponent<BiteScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI () {
		// fuel box background
		Texture2D bgTexture = new Texture2D(1, 1);
		bgTexture.SetPixel(0, 0, Color.red);
		bgTexture.Apply();
		GUISkin bgSkin = GUISkin.CreateInstance<GUISkin>();
		bgSkin.box.normal.background = bgTexture;
		GUI.skin = bgSkin;
		GUI.Box(new Rect(25, 20, 100, 10), GUIContent.none);

		// fuel left box
		Texture2D boxTexture = new Texture2D(1, 1);
		boxTexture.SetPixel(0, 0, Color.green);
		boxTexture.Apply();
		GUISkin boxSkin = GUISkin.CreateInstance<GUISkin>();
		boxSkin.box.normal.background = boxTexture;
		GUI.skin = boxSkin;
		GUI.Box(new Rect(25, 20, (biteScript.fuelLeft > 0 ? biteScript.fuelLeft : 0.1f), 10), GUIContent.none);
	}
}
