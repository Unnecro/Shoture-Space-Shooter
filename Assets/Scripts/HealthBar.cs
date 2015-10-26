using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

  private Vector3 start_pos;
  private Vector3 end_pos;

  private Image cast_image;

  private float fill_time = 2f;

  private RectTransform cast_transform;

	// Use this for initialization
	void Start () {
    this.cast_transform = this.GetComponent<RectTransform>();
    this.cast_image = this.GetComponent<Image>();

    this.end_pos = this.cast_transform.position;
    this.start_pos = new Vector3(
      this.cast_transform.position.x - this.cast_transform.rect.width,
      this.cast_transform.position.y,
      this.cast_transform.position.z
    );

    StartCoroutine(fillHealth());
	}
	
	// Update is called once per frame
	void Update () {
	  
	}

  private IEnumerator fillHealth() {
    this.cast_transform.position = this.start_pos;

    float time_left = Time.deltaTime;
    float rate = 1.0f / this.fill_time;

    float progress = 0.0f;

    while (progress <= 1.0f) {
      this.cast_transform.position = Vector3.Lerp(
        this.start_pos, this.end_pos, progress  
      );

      progress += rate * Time.deltaTime;
      time_left += Time.deltaTime;

      yield return null;
    }

    this.cast_transform.position = end_pos;

  }
}
