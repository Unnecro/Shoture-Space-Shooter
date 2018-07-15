using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

  public GameObject bullet;

  private int maxHealth = 1000;

  private int currentHealth;

  public ShootingArea shootingArea;
  public MovementArea movement_area;

  private float last_y;
  private float current_y;

  private float accumulated_time = 0f;
  private float last_time = 0f;

  private short ship_status = 0;

  private float fire_delay = 0.08f; //Per second
  private float fire_delay_tmp;

  private bool is_shooting = false;

  private float original_x_pos;

  private float recoil = 0f;

  private float shotSpeed = 30f;

  void OnDrawGizmos() {
    Gizmos.DrawWireCube(this.transform.position, new Vector3(this.transform.localScale.x, this.transform.localScale.y));
  }

  // Use this for initialization
  void Start() {
    last_y = this.transform.position.y;

    original_x_pos = this.transform.position.x;
    fire_delay_tmp = fire_delay;

    this.currentHealth = this.maxHealth;
  }

  // Update is called once per frame
  void Update() {
    handleMovement();
    current_y = this.transform.position.y;
    handleShooting();
    handleSprites();

    checkColor();

    last_y = current_y;

    if(this.currentHealth <= 0) {
      Debug.Log("Dead!");

      // TODO Go to menu scene
      this.currentHealth = this.maxHealth;
    }
  }

  void handleSprites() {
    if (last_y < current_y && current_y - last_y > 0.1f) {
      ship_status = 1;
      // TODO rotate ship up

      accumulated_time = 0f;
    } else if (last_y > current_y && current_y - last_y < -0.1f) {
      ship_status = -1;
      // TODO rotate ship down

      accumulated_time = 0f;
    } else {
      float time_elapsed = accumulated_time - last_time;
      if (time_elapsed >= 0.05f || ship_status == 0) {
        ship_status = 0;
        // TODO set ship rotation horizontal

        last_time = Time.deltaTime;
        accumulated_time = 0f;
      } else {
        accumulated_time += Time.deltaTime;
      }
    }
  }

  void handleMovement() {
    Vector3 player_pos = new Vector3(
      original_x_pos,
      this.transform.position.y,
      this.transform.position.z  
    );

    if (is_shooting) {
      player_pos -= new Vector3(recoil, 0f);
    }

    if (movement_area.isInteracting()) {
      float y_pos_units = movement_area.getPosition(player_pos).y;

      player_pos = new Vector3(
        player_pos.x,
        y_pos_units,
        player_pos.z
      );

    }

    this.transform.position = player_pos;
  }

  void handleShooting() {

    if (shootingArea.isInteracting()) {
      is_shooting = true;
      if (fire_delay_tmp >= fire_delay) {
        Vector3 bullet_pos_up = new Vector3(
          this.transform.position.x + 1f,
          this.transform.position.y - 0.5f,
          this.transform.position.z
        );

        Vector3 bullet_pos_down = new Vector3(
          this.transform.position.x + 1f,
          this.transform.position.y + 0.5f,
          this.transform.position.z
        );

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        bulletScript.speedX = this.shotSpeed;
        bulletScript.speedY = shootingArea.getPosition().y;
        bulletScript.player_pos = this.transform.position;

        Instantiate(bullet, bullet_pos_up, Quaternion.identity);
        Instantiate(bullet, bullet_pos_down, Quaternion.identity);

        fire_delay_tmp = 0;
      } else {
        fire_delay_tmp += Time.deltaTime;
      }
    } else {
      is_shooting = false;
      fire_delay_tmp += Time.deltaTime;
    }
    
  }

  void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "Enemy bullet"){
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

    Color newColor = new Color(
      oldColor.r,
      colorValue,
      colorValue,
      oldColor.a
    );

    this.GetComponent<SpriteRenderer>().color = newColor;
  }

}
