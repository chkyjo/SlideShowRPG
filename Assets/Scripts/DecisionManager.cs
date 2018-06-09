using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionManager : MonoBehaviour {

    public GameObject inventoryManager;
    public GameObject settingManager;

    public GameObject decisionPanel;
    public GameObject decisionScroller;
    public RectTransform decisionScroll;
    public Scrollbar scrollBar;

    
    public GameObject throwDecisionObject;
    public GameObject throwItemDecisionObject;
    public GameObject throwItemAtDecisionObject;
    public GameObject talkDecisionObject;
    public GameObject talkToDecisionObject;


    int itemsInDecisionPanel;

	// Use this for initialization
	void Start () {
        itemsInDecisionPanel = 0;
        FillDecisionPanel();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //fill decision panel based on inventory and scene
    private void FillDecisionPanel(){
        //if there is something in the players inventory add the throw action
        if(inventoryManager.GetComponent<Inventory>().GetInventoryCounts()[3] > 0){
            AddDecision("throw");
        }
        //if there are people in the area add the talk action
        if (settingManager.GetComponent<SettingManager>().numCharactersInSetting > 0){
            AddDecision("talk");
        }

        //if there are living things in the area add the attack action
        if (settingManager.GetComponent<SettingManager>().numCharactersInSetting > 0){
            AddDecision("attack");
        }

        //if there are item up for grabs add the take action


        //if there are items available to steal add the steal action


        Debug.Log("Decisions filled");
    }

    //add action to panel
    void AddDecision(string name){

        if(name == "throw"){
            //throwDecisionObject.GetComponentInChildren<Text>().text = "Throw something";
            var obj = GameObject.Instantiate(throwDecisionObject);
            //obj.transform.parent = decisionScroller.transform;
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
            Debug.Log("Added throw action");
        }
        if (name == "talk"){
            //talkDecisionObject.GetComponentsInChildren<Text>().text = "Talk to someone";
            var obj = GameObject.Instantiate(talkDecisionObject);
            //obj.transform.parent = decisionScroller.transform;
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
            Debug.Log("Added talk action");
        }
        /*
        if (name == "attack"){
            talkDecisionObject.GetComponentInChildren<Text>().text = "Attack someone";
            var obj = GameObject.Instantiate(talkDecisionObject);
            //obj.transform.parent = decisionScroller.transform;
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
            Debug.Log("Added attack action");
        }
        */
        scrollBar.value = 1;
        decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, 0);

    }

    //called when user selects the throw action
    public void Throw(){

        ClearDecisionPanel();

        //display available items to throw
        Inventory tempInventory = inventoryManager.GetComponent<Inventory>();
        for (int i = 0; i < tempInventory.GetInventoryCounts()[0]; i++){
            throwItemDecisionObject.GetComponentsInChildren<Text>()[1].text = tempInventory.GetWeapon(i)._name;
            var obj = GameObject.Instantiate(throwItemDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }
        for (int i = 0; i < tempInventory.GetInventoryCounts()[1]; i++){
            throwItemDecisionObject.GetComponentsInChildren<Text>()[1].text = tempInventory.GetArmor(i)._name;
            var obj = GameObject.Instantiate(throwItemDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }
        for (int i = 0; i < tempInventory.GetInventoryCounts()[2]; i++){
            throwItemDecisionObject.GetComponentsInChildren<Text>()[1].text = tempInventory.GetFood(i)._name;
            var obj = GameObject.Instantiate(throwItemDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }

        scrollBar.value = 1;
        if (itemsInDecisionPanel > 7){
            int difference = itemsInDecisionPanel - 7;
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, -(difference * 50));
        }
        else{//else reset
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, 0);
        }

    }

    //called when user selects the item they want to throw
    public void ThrowItem(){

        ClearDecisionPanel();

        //allow user to select who or what they want to throw the item at depending on the scene
        int numCharacters = settingManager.GetComponent<SettingManager>().numCharactersInSetting;
        for (int i = 0; i < numCharacters; i++){
            throwItemAtDecisionObject.GetComponentsInChildren<Text>()[1].text = settingManager.GetComponent<SettingManager>().charactersInSetting[i].GetFirstName()
                + " " + settingManager.GetComponent<SettingManager>().charactersInSetting[i].GetLastName();
            var obj = GameObject.Instantiate(throwItemAtDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }
        //adjust space on scrollbar
        scrollBar.value = 1;
        if (itemsInDecisionPanel > 7){
            int difference = itemsInDecisionPanel - 7;
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, -(difference * 30));
        }
        else{//else reset
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, 0);
        }

    }

    //called when user selects who or what to throw item at
    public void ThrowItemAt(){
        RefreshDecisionList();
        Debug.Log("Object thrown at");
    }

    
    //called when user selects the eat action
    public void Talk(){

        ClearDecisionPanel();

        //allow user to select who to talk to
        int numCharacters = settingManager.GetComponent<SettingManager>().numCharactersInSetting;
        for (int i = 0; i < numCharacters; i++){
            talkToDecisionObject.GetComponentsInChildren<Text>()[1].text = settingManager.GetComponent<SettingManager>().charactersInSetting[i].GetFirstName()
                + " " + settingManager.GetComponent<SettingManager>().charactersInSetting[i].GetLastName();
            var obj = GameObject.Instantiate(talkToDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }

        scrollBar.value = 1;
        if (itemsInDecisionPanel > 7){
            int difference = itemsInDecisionPanel - 7;
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, -(difference * 30));
        }
        else{//else reset
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, 0);
        }

    }

    //called when user selects the item to eat action
    public void TalkTo(string firstName, string lastName){
        RefreshDecisionList();
        Debug.Log("You talked to : " + firstName + " " + lastName);
    }

    //called when user completes the selections for an action
    public void RefreshDecisionList(){
        ClearDecisionPanel();
        FillDecisionPanel();
    }

    private void ClearDecisionPanel(){
        for (int i = 0; i < itemsInDecisionPanel; i++){
            Destroy(decisionScroller.transform.GetChild(i).gameObject);
        }
        itemsInDecisionPanel = 0;
    }
}
