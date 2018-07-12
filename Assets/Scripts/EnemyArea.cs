using UnityEngine;
using System.Collections;

public class EnemyArea : MonoBehaviour {

  private float boundMaxY;
  private float boundMaxX;
  private float boundMinY;
  private float boundMinX;

  void Start() {
    this.boundMinX = this.gameObject.transform.localPosition.x - (this.gameObject.transform.localScale.x / 2);
    this.boundMaxX = this.gameObject.transform.localPosition.x + (this.gameObject.transform.localScale.x / 2);

    this.boundMinY = this.gameObject.transform.localPosition.y - this.gameObject.transform.localScale.y / 2;
    this.boundMaxY = this.gameObject.transform.localPosition.y + this.gameObject.transform.localScale.y / 2;    
  }

	void OnDrawGizmos() {
    Gizmos.DrawWireCube(this.transform.position, new Vector3(this.transform.localScale.x, this.transform.localScale.y));
  }
  public Vector3 fixPosition(Vector3 enemyPosition, Vector3 enemyColliderScale) {
    float moveX = Mathf.Clamp(
      enemyPosition.x,
      this.boundMinX + enemyColliderScale.x / 2,
      this.boundMaxX - enemyColliderScale.x / 2
    );

    float moveY = Mathf.Clamp(
      enemyPosition.y,
      this.boundMinY + enemyColliderScale.y / 2,
      this.boundMaxY - enemyColliderScale.y / 2
    );

    return new Vector3(
      moveX,
      moveY
    );
  }

}
