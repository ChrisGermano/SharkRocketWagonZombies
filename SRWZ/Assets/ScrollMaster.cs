using UnityEngine;
using System.Collections;

public class ScrollMaster : MonoBehaviour {

	public float scroll_speed = 0.5f;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		//Scroll the grass on the ground
		float offset = Time.time * scroll_speed * -2;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
	}
}
