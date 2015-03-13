using UnityEngine;
using System.Collections;

public class basketController : MonoBehaviour {
	
	public Camera mCamera;
	public float maxWidth;
	private bool canControl;
	public Texture2D left;
	public Texture2D right;
	// Use this for initialization
	void Start () {
		if (mCamera == null) {
			mCamera = Camera.main;
		}
		canControl = false;
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);;
		Vector3 targetWidth = mCamera.ScreenToWorldPoint (upperCorner);
		float basketWidth = GetComponent<Renderer> ().bounds.extents.x;
		maxWidth = targetWidth.x - basketWidth;
		
	}
	void OnGUI () {
		
		if (canControl) {
			if (GUI.RepeatButton (new Rect (10, Screen.height / 2, 200, 200), left)) {
				Vector3 targetPosition = new Vector3 (transform.position.x - 0.05f, 0.0f, 0.0f);
				float targetWidth = Mathf.Clamp (targetPosition.x, -maxWidth, maxWidth);
				targetPosition = new Vector3 (targetWidth, targetPosition.y, targetPosition.z);
				transform.position = targetPosition;
			}
			if (GUI.RepeatButton (new Rect (Screen.width - 210, Screen.height / 2, 200, 200), right)) {
				Vector3 targetPosition = new Vector3 (transform.position.x + 0.05f, 0.0f, 0.0f);
				float targetWidth = Mathf.Clamp (targetPosition.x, -maxWidth, maxWidth);
				targetPosition = new Vector3 (targetWidth, targetPosition.y, targetPosition.z);
				transform.position = targetPosition;
			}
		}
	}

	public void toggleControl(bool toggle){
		canControl = toggle;
	}
}
