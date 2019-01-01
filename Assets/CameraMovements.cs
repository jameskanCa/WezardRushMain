using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovements : MonoBehaviour {

	public IEnumerator shake (float duration, float magnitude){
		Vector3 originalPos = transform.localPosition;
		Debug.Log("initial" + originalPos);
		float elapsed = 0.0f;
		while(elapsed < duration){
			float x = Random.Range(-1f, 1f) * magnitude;
			float y = Random.Range(-1f, 1f) * magnitude;

			transform.localPosition = new Vector3(x,y,originalPos.z);
			elapsed += Time.deltaTime;
		
			yield return null;
		}
		Debug.Log("item");
			transform.localPosition = originalPos;
		Debug.Log("after" + transform.localPosition);
		Debug.Log("after");
	}
	
}
