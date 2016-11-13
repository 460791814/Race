using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {
    private UIInput nameInput;
	// Use this for initialization
	void Start () {
        nameInput = transform.Find("username").GetComponent<UIInput>();
        if (PlayerPrefs.HasKey("name"))
        {
            nameInput.value = PlayerPrefs.GetString("name");
        }
	}

    public void OnButtonClick()
    {
        PlayerPrefs.SetString("name", nameInput.value);
        Application.LoadLevel(1);
    }
}
