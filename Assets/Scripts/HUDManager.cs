using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {

	private int world_units = 16;
	public static float px_unit = 60;
	public static float screen_units_height;
	public static float limit_spacing_y = 0.45f;

	void Awake(){
		px_unit = ((Screen.height / 2f) / Camera.main.orthographicSize);
		screen_units_height = Screen.height / px_unit;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// if(Input.touchCount > 0){
		// 	if()
		// 	// Input.GetTouch(0).position.x
		// 	print(Screen.width);
		// }
	}
}
