using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject deletePromptPanel;
    public GameObject itemsPanel;
    public GameObject inventoryManager;
    public Button weaponButton;
    public Button armorButton;
    public Button foodButton;

    private string selected;

    public int itemsOnDisplay;

	// Use this for initialization
	void Start () {
        itemsOnDisplay = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetUpGame(){

        //settingManager.NewWeather();

    }

    public void DeleteItemPrompt(string name){
        for(int i = 0; i < itemsOnDisplay; i++)
        {
            itemsPanel.transform.GetChild(0).GetChild(0).GetChild(i).GetComponentInChildren<Button>().interactable = false;
        }

        weaponButton.interactable = false;
        armorButton.interactable = false;
        foodButton.interactable = false;

        deletePromptPanel.SetActive(true);
        selected = name;
    }

    public void DeleteItem(){
        inventoryManager.GetComponent<Inventory>().DeleteItem(selected);
    }

    public void DisableItemButtons(){
        for (int i = 0; i < itemsOnDisplay; i++){
            itemsPanel.transform.GetChild(0).GetChild(0).GetChild(i).GetComponentInChildren<Button>().interactable = false;
        }

    }
    public void EnableItemButtons(){
        for (int i = 0; i < itemsOnDisplay; i++)
        {
            itemsPanel.transform.GetChild(0).GetChild(0).GetChild(i).GetComponentInChildren<Button>().interactable = true;
        }

        weaponButton.interactable = true;
        armorButton.interactable = true;
        foodButton.interactable = true;
    }

    public void DisableDeletePrompt()
    {
        deletePromptPanel.SetActive(false);
    }

}
