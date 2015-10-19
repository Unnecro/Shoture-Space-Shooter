using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	[HideInInspector]
	public int damage;

  public float speed_y;
  public float target_y;
  public Vector3 player_pos;
  private float recoil = 0.5f;

	void Awake(){
		this.damage = 5;
	}

	// Use this for initialization
	void Start () {
    target_y = player_pos.y - speed_y + Random.Range(-recoil, recoil);

    speed_y = -target_y / (HUDManager.screen_units_width + player_pos.x + 1.5f);
  }
	
	// Update is called once per frame
	void Update () {
		if(
			this.transform.position.x > HUDManager.screen_units_width ||
			this.transform.position.x < 0 ||
			this.transform.position.y > HUDManager.screen_units_height ||
			this.transform.position.y < 0
		){
			Destroy(this.gameObject);
		} else {
			this.transform.position += new Vector3(
				30f * Time.deltaTime,
				speed_y
			);
		}
	}
}
