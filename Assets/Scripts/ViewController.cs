using UnityEngine;
using System.Collections;

public class ViewController : MonoBehaviour {
    public GameObject mainCamera;//第三人称
    public GameObject firstCamera;// 第一人称
    private bool isFirst=false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (isFirst)
            {
                mainCamera.SetActive(true);
                firstCamera.SetActive(false);
                isFirst = false;
            }
            else {
                mainCamera.SetActive(false);
                firstCamera.SetActive(true);
                isFirst = true;
            }
        }
	}
}
