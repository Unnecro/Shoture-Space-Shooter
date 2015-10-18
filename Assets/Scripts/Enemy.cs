using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private int health = 100;
	private float scale = 0.01f;
	private float velocity_x = 0f;
	private float velocity_y = 0f;

	// Use this for initialization
	void Start () {
		this.transform.localScale = new Vector3(scale, scale, scale);
		this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
		
		velocity_x = Random.Range(0.05f, 0.2f);
		velocity_y = Random.Range(0.05f, 0.2f);
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.localScale.x < 1f){
			this.transform.localScale += new Vector3(scale, scale, scale);	
		}

		if(
			this.transform.position.x < 8 ||
			this.transform.position.x > 15
		){
		
			velocity_x = -velocity_x;
		}

		if(
			this.transform.position.y < 0 ||
			this.transform.position.y > 9
		){
			velocity_y = -velocity_y;
		}

		this.transform.position += new Vector3(velocity_x, velocity_y, 0f);
		
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "Bullet"){
			Bullet bullet = collider.gameObject.GetComponent<Bullet>();

			Color old_color = this.GetComponent<SpriteRenderer>().color;
			Color new_color = new Color(
				old_color.r,
				old_color.g - (bullet.damage / 100f),
				old_color.b - (bullet.damage / 100f),
				old_color.a
			);

			this.GetComponent<SpriteRenderer>().color = new_color;

			this.health -= bullet.damage;

			if(this.health <= 0){
				Instantiate(
					this.gameObject,
					new Vector3(
						this.transform.position.x,
						this.transform.position.y,
						this.transform.position.z
					),
					Quaternion.identity
				);


				Destroy(gameObject);

			}

			Destroy(collider.gameObject);

		}
	}
}
