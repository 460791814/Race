using UnityEngine;
using System.Collections;

public class CarIdle : MonoBehaviour {
    public AudioSource engineSource;
  
    public ParticleEmitter leftEmitter;
    public ParticleEmitter rightEmitter;
	// Use this for initialization
	void Start () {
        GameObject car = GameObject.FindGameObjectWithTag("Car").gameObject;
        engineSource = car.GetComponent<AudioSource>();
        leftEmitter = car.transform.Find("LeftSkidSmoke").particleEmitter;
        rightEmitter = car.transform.Find("RightSkidSmoke").particleEmitter;
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(Input.GetAxis("Vertical")) != 0)
        {
            engineSource.pitch = 0.2f + Mathf.Abs(Input.GetAxis("Vertical"));
                leftEmitter.emit=true;
                rightEmitter.emit = true;
        }
        else {
            engineSource.pitch = 0.2f;
            leftEmitter.emit = false;
            rightEmitter.emit = false;
        }
	}

    public void DisabeSelf()
    {
        engineSource.pitch = 0.2f;
        leftEmitter.emit = false;
        rightEmitter.emit = false;
        this.enabled = false;
    }
}
