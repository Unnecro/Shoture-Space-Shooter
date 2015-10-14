using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject bullet;

	// Use this for initialization
	void Start () {
 
	}
	
	// Update is called once per frame
	void Update () {
		handleMovement();
		handleShooting();
	}

	void handleMovement(){
		Vector3 player_pos = this.transform.position;
		bool y_moving = false;
		bool shooting = false;
		float y_pos_px = 0;

		if(Input.touchCount > 0){
			for(int i = 0; i < Input.touchCount; i++){
				if(Input.GetTouch(i).position.x < Screen.width / 2){
					y_moving = true;
					y_pos_px = Input.GetTouch(i).position.y;
				}
			}
		}

		if(y_moving){
			float y_pos_units = y_pos_px / HUDManager.px_unit;

			if(
				y_pos_units > this.transform.position.y &&
				y_pos_units - this.transform.position.y > 0.5f
			){
				y_pos_units = +(this.transform.position.y + 30f) * Time.deltaTime;
				y_pos_units += this.transform.position.y;
			} else if(
				y_pos_units < this.transform.position.y &&
				this.transform.position.y - y_pos_units > 0.5f
			){
				y_pos_units = -(this.transform.position.y + 30f) * Time.deltaTime;
				y_pos_units += this.transform.position.y;
			}

			float y_move = Mathf.Clamp(
				y_pos_units,
				HUDManager.limit_spacing_y,
				HUDManager.screen_units_height - HUDManager.limit_spacing_y
			);

			player_pos = new Vector3(
				player_pos.x,
				y_move,
				player_pos.z
			);
		}

		this.transform.position = player_pos;
	}

	void handleShooting(){
		bool shooting = false;
		if(Input.touchCount > 0){
			for(int i = 0; i < Input.touchCount; i++){
				if(
					Input.GetTouch(i).position.x > Screen.width / 2 &&
					Input.GetTouch(i).position.y > Screen.height / 4 &&
					Input.GetTouch(i).position.y < (Screen.height / 4) * 3

				){
					shooting = true;
				}
			}
		}

		if(shooting){
			Vector3 bullet_pos = new Vector3(
				this.transform.position.x + 1.3f,
				this.transform.position.y,
				this.transform.position.z
			);

			Instantiate(bullet, bullet_pos, Quaternion.identity);
		}
	}

}
