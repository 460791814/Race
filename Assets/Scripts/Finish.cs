using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {
    private TotalTime totalTime;
    void Start()
    {
        totalTime = this.GetComponent<TotalTime>();
    }
	// Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "Car")
        {
            totalTime.Stop();
            float oldTime = 999999;
            if (PlayerPrefs.HasKey("BestTime"))
            {
                oldTime = PlayerPrefs.GetFloat("BestTime");

            }
            totalTime.totalTimeLabel.text = "当前时间：" + totalTime.totalTime + "秒" + "\r\n" + "最佳时间：" + oldTime + "秒";
            if (totalTime.totalTime < oldTime)
            {
                PlayerPrefs.SetFloat("BestTime", totalTime.totalTime);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
