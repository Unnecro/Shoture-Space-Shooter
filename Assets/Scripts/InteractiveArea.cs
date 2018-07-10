using UnityEngine;
using System.Collections;

public abstract class InteractiveArea : MonoBehaviour {

  protected float sprite_pos_x;
  protected float sprite_pos_y;

  protected float sprite_size_x;
  protected float sprite_size_y;

  protected float area_min_x;
  protected float area_max_x;

  protected float area_min_y;
  protected float area_max_y;

  protected bool is_interacting = false;

  protected float input_pos_y;

  protected float input_units_pos_x;
  protected float input_units_pos_y;

  void OnDrawGizmos() {
    Gizmos.DrawWireCube(this.transform.position, new Vector3(this.transform.localScale.x, this.transform.localScale.y));
  }

  // Use this for initialization
  void Start() {

    sprite_pos_x = this.transform.position.x;
    sprite_size_x = this.transform.localScale.x;

    sprite_pos_y = this.transform.position.y;
    sprite_size_y = this.transform.localScale.y;

    area_min_x = sprite_pos_x - (sprite_size_x / 2);
    area_max_x = sprite_pos_x + (sprite_size_x / 2);

    area_min_y = sprite_pos_y - (sprite_size_y / 2);
    area_max_y = sprite_pos_y + (sprite_size_y / 2);
  }

  public bool isInteracting() {
    bool is_interacting = false;
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
          is_interacting = true;

          break;
        }
      }
    } else if (Input.GetMouseButton(0)) {
      input_units_pos_x = Input.mousePosition.x * HUDManager.screen_units_width / Screen.width;
      input_units_pos_y = Input.mousePosition.y * HUDManager.screen_units_height / Screen.height;
      if (
        input_units_pos_x > area_min_x &&
        input_units_pos_x < area_max_x &&
        input_units_pos_y > area_min_y &&
        input_units_pos_y < area_max_y
      ) {
        is_interacting = true;
      }
    }

    return is_interacting;
  }

}
