using UnityEngine;
using System.Collections;

public class CarSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public  void OnSelectClick () {
        Application.LoadLevel(2);
	}
}
