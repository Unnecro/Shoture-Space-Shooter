using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void restartLevel(){
		Debug.Log("test");
		SceneManager.LoadScene("Game");
	}

	public void QuitGame(){
		Application.Quit();
	}
	
}
