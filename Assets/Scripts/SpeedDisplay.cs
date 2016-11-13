using UnityEngine;
using System.Collections;

public class SpeedDisplay : MonoBehaviour {
    [HideInInspector]
    public WheelCollider wheelflcollider;
    public Transform pointContainer;
    private UILabel speedLabel;
    private float zRotation;
	// Use this for initialization
	void Start () {
        speedLabel = GetComponent<UILabel>();
        zRotation = pointContainer.eulerAngles.z;
        //print(pointContainer.eulerAngles.z + "--" + pointContainer.localEulerAngles.z);值相等
        wheelflcollider = GameObject.FindGameObjectWithTag("Car").transform.Find("WheelColiders/WheelFLColider").GetComponent<WheelCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        float s=wheelflcollider.rpm * (wheelflcollider.radius * 2 * Mathf.PI) * 60 / 1000;//千米每小时
        s = Mathf.Round(s);
        speedLabel.text = s.ToString();
        if (s < 0)
        {
            s = 0;
        }
        float newZRotation = zRotation - s * (270 / 140f);
        pointContainer.eulerAngles = new Vector3(0, 0, newZRotation);
	}
}
