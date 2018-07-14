using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private int defaultDamage = 25;
	[HideInInspector]
	public int damage = 0;

	private float defaultSpeedX = 30f;
	[HideInInspector]
	public float speedX = 0;

	[HideInInspector]
	public float speedY = 0f;
  
  private float target_y;
  public Vector3 player_pos;
  private float recoil = 0.5f;

	void Awake(){
		if(this.damage == 0) {	this.damage = this.defaultDamage; }
		if(this.speedX == 0) {	this.speedX = this.defaultSpeedX; }
	}

	// Use this for initialization
	void Start () {		
    this.target_y = player_pos.y - this.speedY + Random.Range(-recoil, recoil);

    this.speedY = -this.target_y / (HUDManager.screen_units_width + player_pos.x + 1.5f);
  }
	
	// Update is called once per frame
	void Update () {
		if( // Out of HUD bounds
			this.transform.position.x > HUDManager.screen_units_width ||
			this.transform.position.x < 0 ||
			this.transform.position.y > HUDManager.screen_units_height ||
			this.transform.position.y < 0
		){
			Destroy(this.gameObject);
		} else {
			this.transform.position += new Vector3(
				this.speedX * Time.deltaTime,
				this.speedY
			);

      this.transform.Rotate(new Vector3(0f, 0f, this.speedY * 5));
		}
	}
}
