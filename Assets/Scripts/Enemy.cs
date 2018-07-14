using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public EnemyArea enemyArea;

	public GameObject bullet;

  private int maxHealth = 100;
	private int currentHealth;
  // private Vector3 original_scale;
	// private float scale = 0.01f;

  private float min_velocity = 1f;
  private float max_velocity = 3f;
  
	private int moveDelay = 0;
	private int[] moveDirection;
  private float velocity_x = 0f;
	private float velocity_y = 0f;

	private float shotSpeed = 30f;

	private Vector3 initialPosition;
	private Quaternion initialRotation;

	private GameObject originalGameObject;

	private enum directions { idle = 0, up, right, down, left };

	void OnDrawGizmos() {
    Gizmos.DrawWireCube(this.transform.position, new Vector3(this.transform.localScale.x, this.transform.localScale.y));
  }

	// Use this for initialization
	void Start () {

    this.currentHealth = this.maxHealth;

    // original_scale = this.transform.localScale;
		// this.transform.localScale = new Vector3(scale, scale, scale);
		this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
		
		velocity_x = Random.Range(min_velocity, max_velocity);
		velocity_y = Random.Range(min_velocity, max_velocity);

		this.originalGameObject = gameObject;

		this.initialPosition = gameObject.transform.position;
		this.initialRotation = gameObject.transform.rotation;
  }

  // Update is called once per frame
  void Update() {

		if(this.currentHealth <= 0){
			Instantiate(gameObject, this.initialPosition, this.initialRotation, this.originalGameObject.transform.parent);

			Destroy(gameObject);
			
			return;
		}

		// Move object
		this.move();
		this.shoot();

		this.checkColor();
	}

	void move() {

		if(this.moveDelay <= 0) {
			this.moveDirection = this.chooseMoveDirection();

			for(int i = 0; i < this.moveDirection.Length; i++) {
				// this.moveDirection[i] = (int) Enemy.directions.left;
				switch(this.moveDirection[i]) {
					case (int) Enemy.directions.idle:
						if(i == 0) {
							velocity_x = 0;
							velocity_y = 0;
						}
					break;
					case (int) Enemy.directions.up:
						velocity_y = -Random.Range(min_velocity, max_velocity);
					break;
					case (int) Enemy.directions.right:
						velocity_x = Random.Range(min_velocity, max_velocity);
					break;
					case (int) Enemy.directions.down:
						velocity_y = Random.Range(min_velocity, max_velocity);
					break;
					case (int) Enemy.directions.left:
						velocity_x = -Random.Range(min_velocity, max_velocity);
					break;
				}
			}

			this.moveDelay = 100;
		} else {
			this.moveDelay--;
		}

		Vector3 unFixedPosition = this.transform.localPosition + new Vector3(
      velocity_x * Time.deltaTime,
      velocity_y * Time.deltaTime
    );

		Vector3 fixedPosition = enemyArea.fixPosition(unFixedPosition, GetComponent<Collider2D>().bounds.size);

		this.transform.localPosition = fixedPosition;
	}

	int[] chooseMoveDirection() {		
		return new int[] { Random.Range(0, 5), Random.Range(0, 5) };
	}
	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "Bullet"){
			Bullet bullet = collider.gameObject.GetComponent<Bullet>();

      this.applyDamage(bullet.damage);

			Destroy(collider.gameObject);
		}
	}

  void applyDamage(int damage) {
    this.currentHealth -= damage;
  }

  void checkColor() {
    Color oldColor = this.GetComponent<SpriteRenderer>().color;

    float colorValue = (float) this.currentHealth * 1f / (float) this.maxHealth;

    Debug.Log(colorValue);

    Color newColor = new Color(
      oldColor.r,
      colorValue,
      colorValue,
      oldColor.a
    );

    this.GetComponent<SpriteRenderer>().color = newColor;
  }

	private float fireDelay = 0.08f;
	private float fireDelayTmp = 0.08f;

	void shoot() {
		if (this.fireDelayTmp >= this.fireDelay) {
			Vector3 bulletPosition = new Vector3(
				this.transform.position.x - 1f,
				this.transform.position.y,
				this.transform.position.z
			);

			Bullet bulletScript = bullet.GetComponent<Bullet>();

			bulletScript.player_pos = this.transform.position;
			bulletScript.speedX     = -this.shotSpeed;
			bulletScript.speedY     = this.transform.position.y;

			bullet.GetComponent<SpriteRenderer>().flipX = true;

			Instantiate(bullet, bulletPosition, Quaternion.identity);

			fireDelayTmp = 0;
		} else {
			fireDelayTmp += Time.deltaTime;
		}
	}
}
