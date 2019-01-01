using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDontDestroy : MonoBehaviour {

	void Awake(){
		Debug.Log("sound ran");
	 DontDestroyOnLoad(transform.gameObject);
	}
}
