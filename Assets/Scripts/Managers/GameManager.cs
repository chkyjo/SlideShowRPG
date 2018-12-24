using System.Collections;
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
    public GameObject mapButton;
    public GameObject videoPlayer;
    public VideoClip vC;

    public GameObject mapPanel;
    public GameObject playerMarker;
    public GameObject indoorPanel;
    public GameObject nextRoomObject;

    private string selected;
    int _interrupted = 0;

    public int itemsOnDisplay;
    public int lastPlayerAction;

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
        mainTextAreaPanel.SetActive(true);
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().UpdateMainText(text);
        indoorPanel.SetActive(true);

        Room room = GameObject.Find("RoomManager").GetComponent<RoomManager>().GetRoom(6000);
        int[,] locations = room.GetButtonLocation();

        GameObject obj;
        obj = Instantiate(nextRoomObject, new Vector3(locations[0, 0], locations[0, 1], 0), Quaternion.identity);
        obj.GetComponentsInChildren<Text>()[0].text = room._exitTexts[0];
        obj.GetComponent<NextRoomAction>().nextRoomID = room._connectedRooms[0];
        obj.GetComponent<NextRoomAction>().leaveText = room._leaveTexts[0];
        obj.transform.SetParent(indoorPanel.transform, false);
        
        mapButton.SetActive(true);
        decisionPanel.SetActive(true);
        inventoryButton.SetActive(true);
        skipButton.SetActive(false);
        healthBar.SetActive(true);
        hungerBar.SetActive(true);

        StartCoroutine(Intro());
        
    }

    IEnumerator Intro() {
        SettingManager sM = GameObject.Find("SettingManager").GetComponent<SettingManager>();
        CharacterManager cM = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();

        string text;
        string name = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>().GetName();
        
        text = "Well, actually on time I see, " + name + ". Let's begin.";
        cM.GetCharacter(1000).SetGreeting(text);
        while (sM.GetTime()[0] != 7) {
            yield return new WaitForSeconds(0.5f);
        }

        text = "Well it's about time " + name + "! C'mon you lazy idiot, take out your sword.";
        cM.GetCharacter(1000).SetGreeting(text);

        while (sM.GetTime()[0] <= 10) {
            yield return new WaitForSeconds(0.5f);
        }

        text = "Go Eat in the Great Hall.";
        cM.GetCharacter(1000).SetGreeting(text);

        while (sM.GetTime()[0] <= 11) {
            yield return new WaitForSeconds(0.5f);
        }

        text = "Let's go! Time for hunting!";
        cM.GetCharacter(1000).SetGreeting(text);
    }

    public void GrabPlayerInfo() {
        GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>().GrabPlayerInfo();
    }

    public void ProcessAndDisplayMainText(string text) {
        string name = GameObject.Find("PlayerManager").GetComponent<PlayerManager>().GetName();
        if(text.Contains("[name]")){
            text.Insert(text.IndexOf('['), name);
            text.Remove(text.IndexOf('['), 6);
        }

        UpdateMainText(text);
        
    }

    public void UpdateMainText(string text) {
        if (mainTextAreaPanel.transform.childCount > 0) {
            Destroy(mainTextAreaPanel.transform.GetChild(0).gameObject);
        }

        var mainText = Instantiate(mainTextObject);
        mainText.transform.GetChild(0).GetComponent<Text>().text = text;
        mainText.transform.SetParent(mainTextAreaPanel.transform, false);
    }

    public void SetInterrupted() {
        _interrupted = 1;
        StartCoroutine(ResetInterrupted());
    }
    IEnumerator ResetInterrupted() {
        SettingManager sM = GameObject.Find("SettingManager").GetComponent<SettingManager>();
        int startingTime = sM.GetTotalMinutes();
        int endTime = sM.GetTotalMinutes();
        while(endTime < startingTime + 60) {
            yield return new WaitForSeconds(0.5f);
            endTime = sM.GetTotalMinutes();
        }
        _interrupted = 0;
    }
    public int GetInterrupted() {
        return _interrupted;
    }

    public void MarkLocation() {
        //3563 x by 2089 y
        //cell size = 50
        if(mapPanel.transform.childCount > 0) {
            Destroy(mapPanel.transform.GetChild(0).gameObject);
        }
        
        Grid grid = mapPanel.GetComponent<Grid>();
        SettingManager sM = GameObject.Find("SettingManager").GetComponent<SettingManager>();
        
        int[] coords = sM.GetCoordinates();
        cheatPanel.transform.GetChild(1).GetComponent<Text>().text = "Coordinates = " + coords[0].ToString() + " " + coords[1].ToString();
        cheatPanel.transform.GetChild(2).GetComponent<Text>().text = sM.GetRoom().ToString();
        Vector3Int v = new Vector3Int(coords[0], coords[1], 0);
        GameObject obj = Instantiate(playerMarker, v, Quaternion.identity);
        obj.transform.SetParent(mapPanel.transform, false);

        /*
        if(obj.GetComponent<RectTransform>().position.x < 850) {
            mapPanel.GetComponent<RectTransform>().position = new Vector3(1240, -740, 0);
        }else if(obj.GetComponent<RectTransform>().position.x < 1700) {
            mapPanel.GetComponent<RectTransform>().position = new Vector3(500, -740, 0);
        }
        else if (obj.GetComponent<RectTransform>().position.x < 2500) {
            mapPanel.GetComponent<RectTransform>().position = new Vector3(-250, -740, 0);
        }
        */
    }

    public void MoveLeft() {
        Vector3 pos = mapPanel.transform.GetChild(2).GetComponent<RectTransform>().position;
        mapPanel.transform.GetChild(2).GetComponent<RectTransform>().position = new Vector3Int((int)pos.x + 10, (int)pos.y, (int)pos.z);
    }

    public void ZoomIn() {
        if (mapPanel.transform.localScale.x >= 1.6) {
            return;
        }
        mapPanel.transform.localScale += new Vector3(0.2f,0.2f,0);
    }

    public void ZoomOut() {
        if (mapPanel.transform.localScale.x <= 0.7) {
            return;
        }
        mapPanel.transform.localScale -= new Vector3(0.2f, 0.2f, 0);
        
    }

    public void SetLastPlayerAction(int actionID) {
        lastPlayerAction = actionID;
    }
    public int GetLastPlayerAction() {
        return lastPlayerAction;
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
        inventoryManager.GetComponent<InventoryManager>().DeleteItem(selected);
    }

    public void TakeTenHealth() {
        PlayerManager pM = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>();
        pM.SubtractHealth(10);
        pM.UpdateHealth();
        pM.SubtractCalories(10);
        pM.UpdateCalories();
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
