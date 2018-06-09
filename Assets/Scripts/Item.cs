using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    //public GameObject inventoryManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PromptDelete()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().DeleteItemPrompt(this.GetComponentInChildren<Text>().text);
    }

    public void DisableItemButtons()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().DisableItemButtons();
    }

    public void EatFoodItem(){
        GameObject.Find("InventoryManager").GetComponent<Inventory>().EatFoodItem(this.gameObject.GetComponentInChildren<Text>().text);
    }
}
