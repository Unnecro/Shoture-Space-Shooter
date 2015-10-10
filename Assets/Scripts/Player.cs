using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject bullet;

	// Use this for initialization
	void Start () {
 
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = touchMove();
		this.transform.position = position;
	}

	Vector3 touchMove(){
		Vector3 player_pos = this.transform.position;
		bool y_moving = false;
		bool shooting = false;
		float y_pos_px = 0;
		if(Input.touchCount > 0){
			for(int i = 0; i < Input.touchCount; i++){
				if(Input.GetTouch(i).position.x < Screen.width / 2){
					y_moving = true;
					y_pos_px = Input.GetTouch(i).position.y;
				} else {
					shooting = true;
				}
			}
		}

		if(y_moving){
			float y_move = Mathf.Clamp(
				y_pos_px / HUDManager.px_unit,
				HUDManager.limit_spacing_y,
				HUDManager.screen_units_height - HUDManager.limit_spacing_y
			);

			player_pos = new Vector3(
				player_pos.x,
				y_move,
				player_pos.z
			);
		}

		if(shooting){
			Vector3 bullet_pos = new Vector3(
				player_pos.x + 2f,
				player_pos.y,
				player_pos.z
			);
			Instantiate(bullet, bullet_pos, Quaternion.identity);
		}

		return player_pos;
	}

}
