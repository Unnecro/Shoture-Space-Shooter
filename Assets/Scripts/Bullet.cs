using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	[HideInInspector]
	public int damage;

	void Awake(){
		this.damage = 5;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(
			this.transform.position.x > HUDManager.screen_units_width ||
			this.transform.position.x < 0 ||
			this.transform.position.y > HUDManager.screen_units_height ||
			this.transform.position.y < 0
		){
			Destroy(this.gameObject);
		} else {
			this.transform.position = new Vector3(
				transform.position.x + (30f * Time.deltaTime),
				transform.position.y,
				transform.position.z
			);
		}
	}
}
