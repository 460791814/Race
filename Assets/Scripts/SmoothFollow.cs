using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {
    [HideInInspector]
    public Transform target;
    public float height = 3.5f;// 摄像机相对车的高度
    public float distance = 7f;//摄像机相对车的距离

    public float smoothSpeed = 1;//平滑移动的速度
	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Car").transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetForward = target.forward;
        targetForward.y = 0;
        Vector3 currentForward = transform.forward;
        currentForward.y = 0;
        //平滑的得到一个摄像机的前方向的位置。
        Vector3 temp = Vector3.Lerp(currentForward.normalized, targetForward.normalized, smoothSpeed * Time.deltaTime);
        //让摄像机缓慢的移动的到正确的位置 ，这样转弯就可以看到车轮了

        Vector3 targetPos = target.position + Vector3.up * height - temp * distance;
        this.transform.position = targetPos;
        transform.LookAt(target.position);
	}
}
