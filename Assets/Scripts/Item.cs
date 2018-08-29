using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    public int ID;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PromptDelete(){
        GameObject.Find("GameManager").GetComponent<GameManager>().DeleteItemPrompt(GetComponentInChildren<Text>().text);
    }

    public void DisableItemButtons(){
        GameObject.Find("GameManager").GetComponent<GameManager>().DisableItemButtons();
    }

    public void EatFoodItem(){
        GameObject.Find("InventoryManager").GetComponent<InventoryManager>().EatFoodItem(ID);
    }
}
