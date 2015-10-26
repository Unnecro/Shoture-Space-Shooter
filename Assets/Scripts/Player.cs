using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

  public GameObject bullet;

  public ShootingArea shooting_area;
  public MovementArea movement_area;

  public Sprite ship_up;
  public Sprite ship_stable;
  public Sprite ship_down;

  private float last_y;
  private float current_y;

  private float accumulated_time = 0f;
  private float last_time = 0f;

  private short ship_status = 0;

  private float fire_delay = 0.08f; //Per second
  private float fire_delay_tmp;

  private bool is_shooting = false;

  private float original_x_pos;

  private float recoil = 0.2f;

  void OnDrawGizmos() {
    Gizmos.DrawWireCube(this.transform.position, new Vector3(this.transform.localScale.x, this.transform.localScale.y));
  }

  // Use this for initialization
  void Start() {
    last_y = this.transform.position.y;
    this.GetComponent<SpriteRenderer>().sprite = ship_stable;

    original_x_pos = this.transform.position.x;
    fire_delay_tmp = fire_delay;
  }

  // Update is called once per frame
  void Update() {
    handleMovement();
    current_y = this.transform.position.y;
    handleShooting();
    handleSprites();

    last_y = current_y;
  }

  void handleSprites() {
    if (last_y < current_y && current_y - last_y > 0.1f) {
      ship_status = 1;
      this.GetComponent<SpriteRenderer>().sprite = ship_up;

      accumulated_time = 0f;
    } else if (last_y > current_y && current_y - last_y < -0.1f) {
      ship_status = -1;
      this.GetComponent<SpriteRenderer>().sprite = ship_down;

      accumulated_time = 0f;
    } else {
      float time_elapsed = accumulated_time - last_time;
      if (time_elapsed >= 0.05f || ship_status == 0) {
        ship_status = 0;
        this.GetComponent<SpriteRenderer>().sprite = ship_stable;

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

    if (shooting_area.isInteracting()) {
      is_shooting = true;
      if (fire_delay_tmp >= fire_delay) {
        Vector3 bullet_pos = new Vector3(
          this.transform.position.x + 1.3f,
          this.transform.position.y,
          this.transform.position.z
        );

        Bullet bullet_script = bullet.GetComponent<Bullet>();
        bullet_script.speed_y = shooting_area.getPosition().y;
        bullet_script.player_pos = this.transform.position;

        Instantiate(bullet, bullet_pos, Quaternion.identity);

        fire_delay_tmp = 0;
      } else {
        fire_delay_tmp += Time.deltaTime;
      }
    } else {
      is_shooting = false;
      fire_delay_tmp += Time.deltaTime;
    }
    
  }

}
