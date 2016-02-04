using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Menhir[] menhirs;
	public GameObject player;
	private int target = 6;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
//		bool didHit = Physics.Raycast (player.transform.position, player.transform.forward, out hit);
		Ray fromCam = Camera.main.ViewportPointToRay(new Vector2(.5f,.5f));
		bool didHit = Physics.Raycast (fromCam,out hit);

//		if (didHit){
//		Debug.Log ("Hit: " + hit.collider.gameObject.tag);
//		}
		int counter = 0;
		foreach (var item in menhirs){
			if(item.correct && item.completed){
				counter++;
			}
//			Debug.Log (item.tag);
			if (didHit && hit.collider.gameObject.CompareTag(item.tag)) {
//				Debug.Log (hit.collider.gameObject.tag);
				if (Input.GetMouseButtonDown(0))
					item.Sing();
			}
		}
		if(counter == target){
			Debug.Log("Completed!");
		}
	}
}