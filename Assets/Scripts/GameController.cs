using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Menhir[] menhirs;
	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, 200f)) {
			foreach (var item in menhirs) {
				/*if (hit.collider.CompareTag(item.tag)) {
					if (Input.GetMouseButtonDown(0))
						item.Sing();
					break;
				}*/
			}

		}
	}
}
