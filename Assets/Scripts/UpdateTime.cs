using UnityEngine;
using UnityEngine.UI;

public class UpdateTime : MonoBehaviour {

    public GameObject settingManager;
    public Slider slider;
    public Slider sitSlider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void AddTime(){
        settingManager.GetComponent<SettingManager>().AddTime((int)slider.value, 0);
    }

    private void AddSitTime(){
        settingManager.GetComponent<SettingManager>().AddTime((int)sitSlider.value, 0);
    }

    private void SetInactive(){
        this.gameObject.SetActive(false);
    }
}
