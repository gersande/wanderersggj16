using UnityEngine;
using System.Collections;

public class ShowHit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		if (Physics.Raycast (gameObject.transform.position, gameObject.transform.forward, out hit, 200f)) {
			Debug.Log("Hit: " + hit.collider.gameObject.tag);
		}
	}
}
