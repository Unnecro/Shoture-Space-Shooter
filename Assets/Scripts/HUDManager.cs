using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {

	public static float px_unit = 60;
  public static float screen_units_height = 9f;
	public static float screen_units_width = 16f;
	public static float limit_spacing_y = 0.6f;

	void Awake(){
		px_unit = ((Screen.height / 2f) / Camera.main.orthographicSize);

		screen_units_height = HUDManager.pixelsToUnits(Screen.height);
		//screen_units_width = HUDManager.pixelsToUnits(Screen.width);
	}

	// Use this for initialization
	void Start () {
		// ClickableArea a = new ClickableArea("1");
		// ClickableArea b = new ClickableArea("2");
	}
	
	// Update is called once per frame
	void Update () {

	}

	public static float pixelsToUnits(int pixels){
		float units = 0;

		units = (pixels / 2f) / Camera.main.orthographicSize;
		units = pixels / units;

		return units;
	}
}
