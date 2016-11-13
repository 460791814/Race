using UnityEngine;
using System.Collections;

public class CarShaDow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = transform.parent.position + Vector3.up * 5;
        this.transform.localEulerAngles = new Vector3(90, 0, 0);
	}
}
