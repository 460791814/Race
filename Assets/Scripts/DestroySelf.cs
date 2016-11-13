using UnityEngine;
using System.Collections;

public class DestroySelf : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 5f);
	}
	
	 
}
