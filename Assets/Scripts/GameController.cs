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
		if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, 200f)) {
			int counter = 0;
			counter = 0;
			foreach (var item in menhirs){
				if(item.correct && item.completed){
					counter++;
				}
				if (hit.collider.CompareTag(item.tag)) {
					if (Input.GetMouseButtonDown(0))
						item.Sing();
					break;
				}

			}
			if(counter == target){
				Debug.Log("Completed!");
			}

		}

	}
}