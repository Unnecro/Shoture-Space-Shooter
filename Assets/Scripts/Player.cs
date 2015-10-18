using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

  public GameObject bullet;

  public GameObject shooting_area;

  public Sprite ship_up;
  public Sprite ship_stable;
  public Sprite ship_down;

  private float last_y;
  private float current_y;

  private float accumulated_time = 0f;
  private float last_time = 0f;

  private short ship_status = 0;

  void OnDrawGizmos() {
    Gizmos.DrawWireCube(this.transform.position, new Vector3(this.transform.localScale.x, this.transform.localScale.y));
  }

  // Use this for initialization
  void Start() {
    last_y = this.transform.position.y;
    this.GetComponent<SpriteRenderer>().sprite = ship_stable;
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
      if (time_elapsed >= 0.2f || ship_status == 0) {
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
    Vector3 player_pos = this.transform.position;

    if (MovementArea.is_moving) {
      float y_pos_units = MovementArea.getPosition(this.transform.position);

      player_pos = new Vector3(
        player_pos.x,
        y_pos_units,
        player_pos.z
      );

      this.transform.position = player_pos;
    }

  }

  void handleShooting() {

    if (ShootingArea.is_shooting) {

      Vector3 bullet_pos = new Vector3(
        this.transform.position.x + 1.3f,
        this.transform.position.y,
        this.transform.position.z
      );

      Instantiate(bullet, bullet_pos, Quaternion.identity);
    }
  }

}
