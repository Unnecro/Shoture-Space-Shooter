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
			this.transform.position.x > Screen.width ||
			this.transform.position.x < 0 ||
			this.transform.position.y > Screen.height ||
			this.transform.position.y < 0
		){
			DestroyImmediate(this);
		} else {
			this.transform.position = new Vector3(
				transform.position.x + (30f * Time.deltaTime),
				transform.position.y,
				transform.position.z
			);
		}
	}
}
