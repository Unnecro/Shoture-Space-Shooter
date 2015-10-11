using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private int health = 100;
private int i = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider){

		Bullet bullet = collider.gameObject.GetComponent<Bullet>();

		Color old_color = this.GetComponent<SpriteRenderer>().color;
		Color new_color = new Color(
			old_color.r,
			old_color.g - (bullet.damage / 100f),
			old_color.b - (bullet.damage / 100f),
			old_color.a
		);

		print(new_color);

		this.GetComponent<SpriteRenderer>().color = new_color;

		this.health -= bullet.damage;

		if(this.health <= 0){
			Destroy(gameObject);
		}

		Destroy(collider.gameObject);

	}
}
