using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void restartLevel(){
		Application.LoadLevel(Application.loadedLevel);
	}

	public void QuitGame(){
		Application.Quit();
	}

}
