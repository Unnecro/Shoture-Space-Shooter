using UnityEngine;
using System.Collections;

public class MovementArea : InteractiveArea {

  public Vector3 getPosition(Vector3 player_position) {
    float position_y = base.input_units_pos_y;
    float position_x = base.input_units_pos_x;

    if (
        position_y > player_position.y &&
        position_y - player_position.y > 0.5f
      ) {
      position_y = +(player_position.y + 30f) * Time.deltaTime;
      position_y += player_position.y;
    } else if (
      position_y < player_position.y &&
      player_position.y - position_y > 0.5f
    ) {
      position_y = -(player_position.y + 30f) * Time.deltaTime;
      position_y += player_position.y;
    }

    float y_move = Mathf.Clamp(
      position_y,
      HUDManager.limit_spacing_y,
      HUDManager.screen_units_height - HUDManager.limit_spacing_y
    );

    Vector3 position = new Vector3(
      position_x,
      y_move
    );

    return position;
  }
}
