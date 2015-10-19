using UnityEngine;
using System.Collections;

public class ShootingArea : InteractiveArea {

  public Vector3 getPosition() {

    float position_y = base.input_units_pos_y;
    float position_x = base.input_units_pos_x;

    float old_range = base.area_max_y - base.area_min_y;
    float new_range = HUDManager.screen_units_height - 0;

    float y_move = (((position_y - base.area_min_y) * new_range) / old_range) + 0;

    Vector3 position = new Vector3(
      position_x,
      y_move
    );

    return position;
  }
}
