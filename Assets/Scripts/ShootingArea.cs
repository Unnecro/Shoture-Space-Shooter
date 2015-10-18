using UnityEngine;
using System.Collections;

public class ShootingArea : MonoBehaviour {

  float sprite_pos_y;
  float sprite_size_y;

  static float area_min_x;
  static float area_max_x;

  static float area_min_y;
  static float area_max_y;

  public static bool is_shooting = false;
  //Range between 0.0f and 1.0f

  private static float input_units_pos_x;
  private static float input_units_pos_y;

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
    is_shooting = isShooting();
	}

  bool isShooting() {
    bool is_shooting = false;
    if (Input.touchCount > 0) {
      for (int i = 0; i < Input.touchCount; i++) {
        input_units_pos_x = Input.GetTouch(i).position.x * HUDManager.screen_units_width / Screen.width;
        input_units_pos_y = Input.GetTouch(i).position.y * HUDManager.screen_units_height / Screen.height;
        if (
          input_units_pos_x > area_min_x &&
          input_units_pos_x < area_max_x &&
          input_units_pos_y > area_min_y &&
          input_units_pos_y < area_max_y
        ) {
          is_shooting = true;

          break;
        }
      }
    }

    return is_shooting;
  }

  public static float getPosition() {
    float position = 0f;

    float old_range = area_max_y - area_min_y;
    float new_range = 1 - 0;

    float value = (((input_units_pos_y - area_min_y) * new_range) / old_range) + 0;

    return value;
  }
}
