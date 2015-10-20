using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

  private int max_health = 100;
	private int health;
  private Vector3 original_scale;
	private float scale = 0.01f;

  private float min_velocity = 1f;
  private float max_velocity = 3f;
  
  private float velocity_x = 0f;
	private float velocity_y = 0f;

	// Use this for initialization
	void Start () {

    health = max_health;

    original_scale = this.transform.localScale;
		this.transform.localScale = new Vector3(scale, scale, scale);
		this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
		
		velocity_x = Random.Range(min_velocity, max_velocity);
		velocity_y = Random.Range(min_velocity, max_velocity);
  }

  // Update is called once per frame
  void Update() {
    if (this.transform.localScale.x < original_scale.x) {
      this.transform.localScale += new Vector3(scale, 0f);
    }
    if (this.transform.localScale.y < original_scale.y) {
      float scale_y = original_scale.y / original_scale.x * scale;
      this.transform.localScale += new Vector3(0f, scale_y);
    }
    if (
			this.transform.position.x <= 8 ||
			this.transform.position.x >= 13.75f
		){
		
			velocity_x = -velocity_x;
		}

		if(
			this.transform.position.y <= 0.5 ||
			this.transform.position.y >= 8.5
		){
			velocity_y = -velocity_y;
		}

		this.transform.position += new Vector3(
      velocity_x * Time.deltaTime,
      velocity_y * Time.deltaTime
    );
		
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "Bullet"){
			Bullet bullet = collider.gameObject.GetComponent<Bullet>();

			Color old_color = this.GetComponent<SpriteRenderer>().color;
			Color new_color = new Color(
				old_color.r,
				old_color.g - (this.max_health / bullet.damage) * 0.1f,
				old_color.b - (this.max_health / bullet.damage) * 0.1f,
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
