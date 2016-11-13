using UnityEngine;
using System.Collections;

public class NumberCount : MonoBehaviour {

    public int StartCount = 5;
  //  public Driving driving;
    public UILabel showCount;
	// Use this for initialization
	void Start () {
      
      StartCoroutine(ControlCount());
	}

    IEnumerator ControlCount()
    {
        showCount.text = StartCount.ToString();
        print(StartCount.ToString());
        while (StartCount>0)
        {
            StartCount--;
            showCount.text = StartCount.ToString();
            yield return new WaitForSeconds(1f);
          
          
        }
        yield return new WaitForSeconds(0.1f);
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer )
        {
            //如果是window环境下，编辑或者EXE
             GameObject.FindGameObjectWithTag("Car").GetComponent<Driving>().enabled=true;
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            //如果是安卓
           GameObject.FindGameObjectWithTag("Car").GetComponent<AndroidDriving>().enabled = true;
        }
        
        showCount.enabled = false;
        this.GetComponent<TotalTime>().Show();
    }
}
