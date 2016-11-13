using UnityEngine;
using System.Collections;
//车轮的悬挂系统
public class FollowColider : MonoBehaviour {
    public WheelCollider collider;
 
	// Update is called once per frame
	void Update () {
        RaycastHit hit;

        //从车轮位置发射一条面向地面的射线，长度为悬挂系统的最大长度
        if (Physics.Raycast(collider.transform.position, Vector3.down,out hit, collider.radius + collider.suspensionDistance))
        {
            //假设车轮碰到了地面
            transform.position = hit.point + Vector3.up * collider.radius;  
        }
        else {
            //车轮悬空
            transform.position = collider.transform.position - collider.transform.up * collider.suspensionDistance;
        }
	}
}
