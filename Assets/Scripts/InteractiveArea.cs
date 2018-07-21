using UnityEngine;
using System.Collections;

public abstract class InteractiveArea : MonoBehaviour {

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
    this.area_min_x = this.transform.position.x - (this.transform.localScale.x / 2);
    this.area_max_x = this.transform.position.x + (this.transform.localScale.x / 2);

    this.area_min_y = this.transform.position.y - (this.transform.localScale.y / 2);
    this.area_max_y = this.transform.position.y + (this.transform.localScale.y / 2);

    Debug.Log(this.area_min_x);
    Debug.Log(this.area_max_x);
    Debug.Log(this.area_min_y);
    Debug.Log(this.area_max_y);
  }

  public bool isInteracting() {
    Debug.Log(this.area_min_x);
    Debug.Log(this.area_max_x);
    Debug.Log(this.area_min_y);
    Debug.Log(this.area_max_y);
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

      Debug.Log(input_units_pos_x);
      Debug.Log(input_units_pos_y);
      Debug.Log(this.area_min_x);
      Debug.Log(this.area_max_x);
      Debug.Log(this.area_min_y);
      Debug.Log(this.area_max_y);
      if (
        input_units_pos_x > this.area_min_x &&
        input_units_pos_x < this.area_max_x &&
        input_units_pos_y > this.area_min_y &&
        input_units_pos_y < this.area_max_y
      ) {
        is_interacting = true;
      }

      Debug.Log(is_interacting);

      // Debug.Log(HUDManager.screen_units_width);
    }

    return is_interacting;
  }

}
