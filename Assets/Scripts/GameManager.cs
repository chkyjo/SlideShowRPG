using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour {

    public GameObject deletePromptPanel;
    public GameObject itemsPanel;
    public GameObject inventoryManager;
    public Button weaponButton;
    public Button armorButton;
    public Button foodButton;
    public GameObject cheatPanel;
    public GameObject mainTextObject;
    public GameObject mainTextAreaPanel;
    public GameObject decisionPanel;
    public GameObject inventoryButton;
    public GameObject skipButton;
    public GameObject healthBar;
    public GameObject hungerBar;
    public GameObject videoPlayer;
    public VideoClip vC;

    private string selected;

    public int itemsOnDisplay;

    // Use this for initialization
    void Start() {
        itemsOnDisplay = 0;
        //videoPlayer.GetComponent<VideoPlayer>().Play();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("0")) {
            cheatPanel.SetActive(true);
        }
        if (Input.GetKeyDown("9")) {
            cheatPanel.SetActive(false);
        }
    }

    public void SetUpGame() {

        string text = "By the time you were 9 you had been training for 5 years. Your sword was already an extension of your arm. You went everywhere with it. As you slid the blade into your hilt you pondered what the day would be like. Training started in 15 minutes and you were no doubt expected to be there. Gregory, the instructor, would have you publicly shamed if you were late, which was surely better than the punishment of death should you not show up.";
        var mainText = Instantiate(mainTextObject);
        mainText.transform.GetChild(0).gameObject.SetActive(true);
        mainText.transform.GetChild(0).GetComponent<Text>().text = text;
        mainText.transform.SetParent(mainTextAreaPanel.transform, false);

        decisionPanel.SetActive(true);
        inventoryButton.SetActive(true);
        skipButton.SetActive(false);
        healthBar.SetActive(true);
        hungerBar.SetActive(true);
        
    }

    public void GrabPlayerInfo() {
        GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>().GrabPlayerInfo();
    }

    public void DeleteItemPrompt(string name) {
        for (int i = 0; i < itemsOnDisplay; i++) {
            itemsPanel.transform.GetChild(0).GetChild(0).GetChild(i).GetComponentInChildren<Button>().interactable = false;
        }

        weaponButton.interactable = false;
        armorButton.interactable = false;
        foodButton.interactable = false;

        deletePromptPanel.SetActive(true);
        selected = name;
    }

    public void DeleteItem() {
        inventoryManager.GetComponent<Inventory>().DeleteItem(selected);
    }

    public void TakeTenHealth() {
        GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>().health -= 10;
        GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>().UpdateHealth();
        GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>().calories -= 10;
        GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>().UpdateCalories();
    }

    public void Restart() {
        GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>().Restart();
        GameObject.Find("SceneManager").GetComponent<SceneLoader>().Restart();
    }

    public void DisableItemButtons(){
        for (int i = 0; i < itemsOnDisplay; i++){
            itemsPanel.transform.GetChild(0).GetChild(0).GetChild(i).GetComponentInChildren<Button>().interactable = false;
        }

    }
    public void EnableItemButtons(){
        for (int i = 0; i < itemsOnDisplay; i++){
            itemsPanel.transform.GetChild(0).GetChild(0).GetChild(i).GetComponentInChildren<Button>().interactable = true;
        }

        weaponButton.interactable = true;
        armorButton.interactable = true;
        foodButton.interactable = true;
    }

    public void DisableDeletePrompt(){
        deletePromptPanel.SetActive(false);
    }

    public void Quit() {
        Application.Quit();
    }

}
