using UnityEngine;
using UnityEngine.UI;

public class UpdateTime : MonoBehaviour {

    public GameObject settingManager;
    public Slider slider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void AddTime(){
        settingManager.GetComponent<SettingManager>().AddTime((int)slider.value);
    }

    private void SetInactive()
    {
        this.gameObject.SetActive(false);
    }
}
