using UnityEngine;
using System.Collections;

public class EnemyArea : MonoBehaviour {

	void OnDrawGizmos() {
    Gizmos.DrawWireCube(this.transform.position, new Vector3(this.transform.localScale.x, this.transform.localScale.y));
  }
  public Vector3 getPosition(Vector3 enemyPosition) {
    float position_y = enemyPosition.y;
    float position_x = enemyPosition.x;

    float y_move = Mathf.Clamp(
      position_y,
      0,
      HUDManager.screen_units_height
    );

		float x_move = Mathf.Clamp(
      position_x,
      0,
      HUDManager.screen_units_width
    );

    Vector3 position = new Vector3(
      x_move,
      y_move
    );

    return position;
  }
}
