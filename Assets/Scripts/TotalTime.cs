using UnityEngine;
using System.Collections;

public class TotalTime : MonoBehaviour {
    public UILabel totalTimeLabel;
    public  float totalTime;
    private bool isStart = false;
	// Use this for initialization
	void Start () {
    
        totalTimeLabel.enabled = false;
        totalTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (isStart)
        {
            totalTime += Time.deltaTime;
            totalTimeLabel.text = totalTime.ToString();

        }
	}
    public void Show()
    {
        isStart = true;
        totalTimeLabel.enabled = true;
    }
    public void Stop()
    {
        isStart = false;
      //  totalTimeLabel.enabled = false;
    }
}
