using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {
    private Vector3 offsetPosition;
    public Transform car;
	// Use this for initialization
	void Start () {
        offsetPosition = transform.position - car.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = car.position + offsetPosition;
	}
}
