using UnityEngine;
using System.Collections;

public class ShootingArea : MonoBehaviour {

  Vector2 shooting_area;
  float sprite_pos_y;
  float sprite_size_y;

  float area_min_x;
  float area_max_x;

  float area_min_y;
  float area_max_y;

  public static bool is_shooting = false;
  //Range between 0.0f and 1.0f
  public static float finger_pos_y;

  private float finger_pos_px_y;

  // Use this for initialization
  void Start () {

    sprite_pos_y  = this.transform.position.y;
    sprite_size_y = this.GetComponent<SpriteRenderer>().bounds.size.y;

    area_min_x = HUDManager.screen_units_width / 2;
    area_max_x = HUDManager.screen_units_width;

    area_max_y = sprite_pos_y + (sprite_size_y / 2);
    area_min_y = sprite_pos_y - (sprite_size_y / 2);
  }
	
	// Update is called once per frame
	void Update () {
    if (isShooting()) {

    }
    finger_pos_y = getPosition();
	}

  bool isShooting() {
    is_shooting = false;
    finger_pos_px_y = 0f;
    if (Input.touchCount > 0) {
      for (int i = 0; i < Input.touchCount; i++) {
        float input_units_pos_x = Input.GetTouch(i).position.x * HUDManager.screen_units_width / Screen.width;
        float input_units_pos_y = Input.GetTouch(i).position.y * HUDManager.screen_units_height / Screen.height;
        if (
          input_units_pos_x > area_min_x &&
          input_units_pos_x < area_max_x &&
          input_units_pos_y > area_min_y &&
          input_units_pos_y < area_max_y
        ) {
          finger_pos_px_y = input_units_pos_y;
          is_shooting = true;
        }
      }
    }

    return is_shooting;
  }

  float getPosition() {
    float position = 0f;
    

    return position;
  }
}
