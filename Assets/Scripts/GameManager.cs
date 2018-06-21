using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject deletePromptPanel;
    public GameObject itemsPanel;
    public GameObject inventoryManager;
    public Button weaponButton;
    public Button armorButton;
    public Button foodButton;
    public GameObject cheatPanel;

    private string selected;

    public int itemsOnDisplay;

    // Use this for initialization
    void Start() {
        itemsOnDisplay = 0;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("f")) {
            cheatPanel.SetActive(true);
        }
        if (Input.GetKeyDown("g")) {
            cheatPanel.SetActive(false);
        }
    }

    public void SetUpGame() {

        //settingManager.NewWeather();

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
