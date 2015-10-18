using UnityEngine;
using System.Collections;

public class MovementArea : MonoBehaviour {

  float sprite_pos_y;
  float sprite_size_y;

  float area_min_x;
  float area_max_x;

  float area_min_y;
  float area_max_y;

  public static bool is_moving = false;

  public static float input_pos_y;

  private static float input_units_pos_x;
  private static float input_units_pos_y;

  // Use this for initialization
  void Start() {

    sprite_pos_y = this.transform.position.y;
    sprite_size_y = this.GetComponent<SpriteRenderer>().bounds.size.y;

    area_min_x = 0f;
    area_max_x = HUDManager.screen_units_width / 2;

    area_max_y = sprite_pos_y + (sprite_size_y / 2);
    area_min_y = sprite_pos_y - (sprite_size_y / 2);
  }

  // Update is called once per frame
  void Update() {
    is_moving = isMoving();
  }

  bool isMoving() {
    bool is_moving = false;
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
          is_moving = true;
          
          break;
        }
      }
    }

    return is_moving;
  }

  public static float getPosition(Vector3 player_position) {
    float position = input_units_pos_y;

    if (
        position > player_position.y &&
        position - player_position.y > 0.5f
      ) {
      position = +(player_position.y + 30f) * Time.deltaTime;
      position += player_position.y;
    } else if (
      position < player_position.y &&
      player_position.y - position > 0.5f
    ) {
      position = -(player_position.y + 30f) * Time.deltaTime;
      position += player_position.y;
    }

    float y_move = Mathf.Clamp(
      position,
      HUDManager.limit_spacing_y,
      HUDManager.screen_units_height - HUDManager.limit_spacing_y
    );

    return y_move;
  }
}
