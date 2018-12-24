using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour {

    public int missionID;
    public int progressID;
    public GameObject conversationBackgroundPanel;
    public GameObject progressMission;
    public GameObject characterResponseObject;
    public GameObject chanceDecisionObject;
    public GameObject decisionScroll;
    
    public GameObject exclaimationPanel;
    public GameObject exclaimationObject;
    public GameObject mainTextObject;
    public GameObject mainTextAreaPanel;

    public GameObject indoorPanel;

    public GameObject SpeechAlertObject;
    public GameObject speechAlertPanel;
    public GameObject exlamationAlertPanel;

    int currentMission;

    public int currentTime = 0;
    public int fireAtDeer = 1;
    int _targetRoom = -1;
    int _guide = 0;
    int killedDeer = 0;
    int desertedMission = 0;

    SettingManager sM;
    PlayerManager pM;
    DecisionManager dM;
    GameManager gM;
    CharacterManager cM;
    ConversationManager convoM;
    RoomManager rM;
    Coroutine CO;

    public void Start() {
        sM = GameObject.Find("SettingManager").GetComponent<SettingManager>();
        pM = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        dM = GameObject.Find("DecisionManager").GetComponent<DecisionManager>();
        gM = GameObject.Find("GameManager").GetComponent<GameManager>();
        cM = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();
        convoM = GameObject.Find("ConversationManager").GetComponent<ConversationManager>();
        rM = GameObject.Find("RoomManager").GetComponent<RoomManager>();
    }

    public void UseMissionManager() {
        GameObject.Find("MissionManager").GetComponent<MissionManager>().StartMission(missionID);
    }

    public void StartMission(int ID) {
        switch (ID) {
            case 1:
                currentMission = 1;
                CO = StartCoroutine(HuntingMission());
                break;
            case 2:
                currentMission = 2;
                break;
        }
    }

    public void SendMissionManagerNewProgress() {
        GameObject.Find("MissionManager").GetComponent<MissionManager>().UpdateProgress(progressID);
    }

    public void UpdateProgress(int progID) {
        progressID = progID;
    }

    public int GetCurrentMission() {
        return currentMission;
    }

    IEnumerator HuntingMission() {

        pM.SetPerception(0);

        string greeting = "Alright, let's meet in the woods on the other side of the archway.";
        StartCoroutine(convoM.DisplayGreeting(1000, greeting));

        yield return new WaitForSeconds(3f);
        progressID = -1;

        if (conversationBackgroundPanel.active == true) {
            conversationBackgroundPanel.SetActive(false);
            convoM.ClearConversation();
        }

        Character tempChar = cM.GetCharacter(1000);
        tempChar.SetLocation(848);
        tempChar = cM.GetCharacter(1001);
        tempChar.SetLocation(848);
        tempChar = cM.GetCharacter(1002);
        tempChar.SetLocation(848);
        tempChar = cM.GetCharacter(1003);
        tempChar.SetLocation(848);

        while (sM.GetRoom() != 848) {
            yield return new WaitForSeconds(.5f);
        }

        string text = pM.GetName() + ", you go with Akaysha and Ben. Jasmine and I will head north. We'll meet back up 10 miles east.";
        DisplaySpeechAlert(-1, 1000, "Gregory", text);

        cM.GetCharacter(1000).SetLocation(748);
        cM.GetCharacter(1001).SetLocation(748);
        pM.AddFollowers(1002);
        pM.AddFollowers(1003);

        while (speechAlertPanel.transform.childCount > 0) {
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(2f);

        text = "Alright, we should head south and then west.";
        DisplaySpeechAlert(-1, 1002, "Akaysha", text);

        indoorPanel.transform.GetChild(3).GetChild(0).GetComponent<Button>().image.color = new Color(1f, 0.5f, 0.1f);

        while(sM.GetRoom() != 948) {
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1f);

        text = "Let's keep going south.";
        DisplaySpeechAlert(-1, 1002, "Akaysha", text);

        indoorPanel.transform.GetChild(3).GetChild(0).GetComponent<Button>().image.color = new Color(1f, 0.5f, 0.1f);

        while (sM.GetRoom() != 1048) {
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1f);

        text = "Alright, now let's head east. Remember to observe for deer tracks.";
        DisplaySpeechAlert(-1, 1002, "Akaysha", text);

        indoorPanel.transform.GetChild(2).GetChild(0).GetComponent<Button>().image.color = new Color(1f, 0.5f, 0.1f);

        int currentRoom = sM.GetRoom();
        while (sM.GetRoom() != currentRoom + 1) {
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1f);

        text = "Let's take a second to look around.";
        DisplaySpeechAlert(-1, 1002, "Akaysha", text);
        dM.AddInterruption(20, 1002, "Where are you going?");

        //wait until the player observes
        while(gM.GetLastPlayerAction() != 2 || gM.GetLastPlayerAction() != 20) {
            yield return new WaitForSeconds(0.5f);
        }
        if(gM.GetLastPlayerAction() == 20) {
            StartCoroutine(DesertHuntingMission());
        }

        //add the follow tracks tutorial option to the list and room
        dM.AddTutorialOption(100, new Color(0.3f, 0.1f, 0.1f), new Color(1f, 1f, 1f), "Follow deer tracks");
        rM.AddTempOptionToRoom(sM.GetRoom(), 100);

        yield return new WaitForSeconds(1f);

        text = "You found tracks. Let's follow them.";
        DisplaySpeechAlert(-1, 1002, "Akaysha", text);

        //add an interrupt for trying to leave
        dM.AddInterruption(20, 1002, "Where are you going!");

        //wait until the player follows the track
        while (gM.GetLastPlayerAction() != 100) {
            yield return new WaitForSeconds(0.5f);
        }

        text = "You discern that the tracks lead south.";
        UpdateMainText(text);
        //add interruptions for going west, north, or east
        dM.AddInterruption(21, 1002, "If you wander off they'll think you're a deserter!");
        dM.AddInterruption(22, 1002, "If you wander off they'll think you're a deserter!");
        dM.AddInterruption(23, 1002, "If you wander off they'll think you're a deserter!");
        //remove the interrupt for leaving
        dM.RemoveInterruption(20);

        //add the option to the next room and remove it from the current
        currentRoom = sM.GetRoom();
        rM.AddTempOptionToRoom(currentRoom + 100, 100);
        rM.RemoveTempOptionFromRoom(currentRoom, 100);
        //reset the player's last action
        gM.SetLastPlayerAction(0);

        //wait until player follows tracks
        while (gM.GetLastPlayerAction() != 100) {
            yield return new WaitForSeconds(0.5f);
        }

        text = "You follow the trail until you spot it about a 100 feet away. It was a buck, looking to be about 250 pounds.";
        UpdateMainText(text);

        //add the shoot option to the room and the decision scroll and remove the follow option
        rM.AddTempOptionToRoom(sM.GetRoom(), 101);
        rM.RemoveTempOptionFromRoom(sM.GetRoom(), 100);
        dM.AddTutorialOption(101, new Color(0.7f, 0, 0), new Color(1, 1, 1), "Take the shot");
        //add an interrupt for trying to leave and remove other interrupts
        dM.AddInterruption(20, 1002, "Where are you going?");
        dM.RemoveInterruption(21);
        dM.RemoveInterruption(22);
        dM.RemoveInterruption(23);

        yield return new WaitForSeconds(1f);

        text = "There it is... Use this bow and take the shot.";
        DisplaySpeechAlert(-1, 1002, "Akaysha", text);

        while (gM.GetLastPlayerAction() != 101) {
            yield return new WaitForSeconds(0.5f);
        }

        rM.RemoveTempOptionFromRoom(sM.GetRoom(), 101);
        dM.RemoveInterruption(20);

        text = "You land a direct hit to the neck.";
        UpdateMainText(text);

        yield return new WaitForSeconds(1f);

        text = "Wow, mildly impressive. Let's meet up with Gregory. We'll come back for the deer.";
        DisplaySpeechAlert(-1, 1002, "Akaysha", text);
        killedDeer = 1;

        cM.GetCharacter(1000).SetLocation(852);
        cM.GetCharacter(1001).SetLocation(852);

        StartGuide(852);

        while (sM.GetRoom() != _targetRoom || sM.GetTime()[0] != 17) {
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);

        if(sM.GetTime()[0] == 17) {
            text = "We're going to be late for the feast. Let's head back. Hopefully Gregory isn't too furious.";
            DisplaySpeechAlert(-1, 1002, "Akaysha", text);
            StartCoroutine(HeadBack());
            yield return null;
        }

        text = pM.GetName() + " got us a deer.";
        DisplaySpeechAlert(-1, 1002, "Akaysha", text);

        while (speechAlertPanel.active == true) {
            yield return new WaitForSeconds(0.5f);
        }
        

        if(cM.GetCharacter(1002).GetStatus() == "DEAD" && cM.GetCharacter(1003).GetStatus() == "DEAD") {
            text = pM.GetName() + "Where the hell are Akaysha and Ben?";
            DisplaySpeechAlert(-1, 1000, "Gregory", text);
        }
        else {
            text = pM.GetName() + "Good. All of you grab some of this pirva. The shiny blue plants.";
            DisplaySpeechAlert(-1, 1000, "Gregory", text);
        }

        
    }

    IEnumerator HeadBack() {

        while (speechAlertPanel.active == true) {
            yield return new WaitForSeconds(0.5f);
        }
        if(killedDeer == 1) {
            string text = "We should go back for the deer.";
            DisplaySpeechAlert(-1, 1002, "Akaysha", text);
        }
        StartGuide(1049);

        while (sM.GetRoom() != _targetRoom || sM.GetTime()[0] != 19) {
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);

        if(killedDeer == 1 && sM.GetTime()[0] == 19) {
            string text = "We have to leave the deer. Let's get to the feast.";
            DisplaySpeechAlert(-1, 1002, "Akaysha", text);
        }
    }

    IEnumerator DesertHuntingMission() {
        pM.RemoveFollower(1002);
        pM.RemoveFollower(1003);
        cM.GetCharacter(1000).SetLocation(6003);
        cM.GetCharacter(1001).SetLocation(6003);
        cM.GetCharacter(1002).SetLocation(6003);
        cM.GetCharacter(1003).SetLocation(6003);
        pM.SetDeserterStatus(1);

        yield return null;
    }

    public void StartGuide(int targetRoom) {
        _guide = 1;
        _targetRoom = targetRoom;

        int targetI = _targetRoom % 100;
        int targetJ = _targetRoom / 100;
        int currentRoom = sM.GetRoom();
        int i = currentRoom % 100;
        int j = currentRoom / 100;

        if((targetI - i)*(targetI - i) > (targetJ - j)*(targetJ - j)) {
            if(i < targetI) {
                indoorPanel.transform.GetChild(2).GetChild(0).GetComponent<Button>().image.color = new Color(1f, 0.5f, 0.1f);
            }
            else {
                indoorPanel.transform.GetChild(0).GetChild(0).GetComponent<Button>().image.color = new Color(1f, 0.5f, 0.1f);
            }
        }
        else {
            if(j < targetJ) {
                indoorPanel.transform.GetChild(3).GetChild(0).GetComponent<Button>().image.color = new Color(1f, 0.5f, 0.1f);
            }
            else {
                indoorPanel.transform.GetChild(1).GetChild(0).GetComponent<Button>().image.color = new Color(1f, 0.5f, 0.1f);
            }
        }
    }
    public void Guide() {

        int targetI = _targetRoom % 100;
        int targetJ = _targetRoom / 100;
        int currentRoom = sM.GetRoom();
        if(_targetRoom == currentRoom) {
            _guide = 0;
            _targetRoom = 0;
            return;
        }
        int i = currentRoom % 100;
        int j = currentRoom / 100;

        if ((targetI - i) * (targetI - i) > (targetJ - j) * (targetJ - j)) {
            if (i < targetI) {
                indoorPanel.transform.GetChild(2).GetChild(0).GetComponent<Button>().image.color = new Color(1f, 0.5f, 0.1f);
            }
            else {
                indoorPanel.transform.GetChild(0).GetChild(0).GetComponent<Button>().image.color = new Color(1f, 0.5f, 0.1f);
            }
        }
        else {
            if (j < targetJ) {
                indoorPanel.transform.GetChild(3).GetChild(0).GetComponent<Button>().image.color = new Color(1f, 0.5f, 0.1f);
            }
            else {
                indoorPanel.transform.GetChild(1).GetChild(0).GetComponent<Button>().image.color = new Color(1f, 0.5f, 0.1f);
            }
        }
    }

    public int GetTargetRoom() {
        return _targetRoom;
    }
    public int GetGuide() {
        return _guide;
    }

    public void DisplaySpeechAlert(int convoID, int characterID, string nameLabel, string displayText) {
        if(cM.GetCharacter(characterID).GetStatus() != "DEAD") {
            SpeechAlertObject.GetComponent<SpeechAlert>().convoID = convoID;
            SpeechAlertObject.GetComponent<SpeechAlert>().characterID = characterID;
            SpeechAlertObject.transform.GetChild(2).GetComponent<Text>().text = nameLabel;
            SpeechAlertObject.transform.GetChild(3).GetComponent<Text>().text = displayText;
            GameObject speechObject = Instantiate(SpeechAlertObject);
            speechAlertPanel.SetActive(true);
            speechObject.transform.SetParent(speechAlertPanel.transform, false);
        }
    }

    IEnumerator HuntingMissionTwo() {
        string text;
        bool startedPerimeter = false;

        while (startedPerimeter == false) {
            if (progressID == 0) {
                startedPerimeter = true;
                //cM.GetCharacter(1002).SetLocation(8);
                //cM.GetCharacter(1003).SetLocation(8);
                text = "You and your companions carefully patrol left, creating a perimeter of 1000 feet. All you can see are trees in all directions. You have not even seen a bit of wildlife. The ground was barron and claimed by only the roots of the tightly packed trees.";
                //dM.NextRoom(8, text);
            }
            yield return new WaitForSeconds(.5f);
        }

        progressID = 1;
        yield return new WaitForSeconds(15f);

        SpeechAlertObject.GetComponent<SpeechAlert>().convoID = 4;
        SpeechAlertObject.GetComponent<SpeechAlert>().characterID = 1003;
        SpeechAlertObject.GetComponent<SpeechAlert>().text = "I'm scared...";
        SpeechAlertObject.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Ben";
        SpeechAlertObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "I'm scared...";
        GameObject speechObject = Instantiate(SpeechAlertObject);
        speechAlertPanel.SetActive(true);
        speechObject.transform.SetParent(speechAlertPanel.transform, false);


        cM.GetCharacter(1002).SetGreeting("What...");
        cM.GetCharacter(1003).SetGreeting("What...");

        yield return new WaitForSeconds(20f);

        SpeechAlertObject.GetComponent<SpeechAlert>().convoID = 2;
        SpeechAlertObject.GetComponent<SpeechAlert>().characterID = 1002;
        SpeechAlertObject.GetComponent<SpeechAlert>().text = "...";
        SpeechAlertObject.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Akaysha";
        SpeechAlertObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "...";
        speechObject = Instantiate(SpeechAlertObject);
        speechAlertPanel.SetActive(true);
        speechObject.transform.SetParent(speechAlertPanel.transform, false);

        yield return new WaitForSeconds(20f);

        text = "The cold, dry air bit your cheeks. Dry twigs snapped loudly under your feet.";
        UpdateMainText(text);

        yield return new WaitForSeconds(20f);

        conversationBackgroundPanel.gameObject.SetActive(false);

        text = "Then you see it; a deer stood at the top of a hill, barely visible through the trees about 100 feet away. Its ears were perked up and its neck stood straight.";
        UpdateMainText(text);

        yield return new WaitForSeconds(8f);

        SpeechAlertObject.GetComponent<SpeechAlert>().convoID = 5;
        SpeechAlertObject.GetComponent<SpeechAlert>().characterID = 1002;
        SpeechAlertObject.GetComponent<SpeechAlert>().text = "I'm gonna take the shot.";
        SpeechAlertObject.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Akaysha";
        SpeechAlertObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "I'm gonna take the shot.";
        speechObject = Instantiate(SpeechAlertObject);
        speechAlertPanel.SetActive(true);
        speechObject.transform.SetParent(speechAlertPanel.transform, false);

        yield return new WaitForSeconds(20f);

        if (fireAtDeer == 1) {
            text = "Akaysha releases an arrow which glides passed the trees and pierces the deer under the neck. It doesn't move or cry out. Moments later an arrow makes a thud on a tree right by your head.";
            UpdateMainText(text);
            sM.combat = 1;
            StartCoroutine(sM.SecondsTimer(15));
            while (sM.combat == 1 && currentTime != 15) {
                if (currentTime == 3) {
                    //akaysha yells to run
                    var ex = Instantiate(exclaimationObject);
                    ex.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Akaysha!";
                    ex.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Run!";
                    exclaimationPanel.SetActive(true);
                    ex.transform.SetParent(exclaimationPanel.transform, false);
                }

                if (sM.combat == 1) {
                    yield return new WaitForSeconds(.2f);
                }
            }
        }
        else {
            SpeechAlertObject.GetComponent<SpeechAlert>().convoID = -1;
            SpeechAlertObject.GetComponent<SpeechAlert>().characterID = 1002;
            SpeechAlertObject.GetComponent<SpeechAlert>().text = "Let's get out of here then.";
            SpeechAlertObject.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Akaysha";
            SpeechAlertObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Let's get out of here then.";
            speechObject = Instantiate(SpeechAlertObject);
            speechAlertPanel.SetActive(true);
            speechObject.transform.SetParent(speechAlertPanel.transform, false);

            yield return new WaitForSeconds(3);

            text = "A little more cautious now you and your group start heading back the way you came.";
            UpdateMainText(text);

        }

        if (sM.combat == 1) {
            text = "An arrow wizzes by your neck and hits Akaysha in the back. You can see the pointy end protrude out her chest as she spins and falls. Another sickening sound as a second arrow hits Ben in the neck.";
            UpdateMainText(text);
            cM.GetCharacter(1002).SetStatus("Dead");
            cM.GetCharacter(1003).SetStatus("Dead");

            for (int i = 0; i < 10; i++) {
                yield return new WaitForSeconds(1f);
            }

            if (sM.combat == 1) {
                sM.combat = 0;
                int gender = pM.gender;
                string[] genders = { "boy", "girl" };
                text = "Men approach you from behind nearby trees. \"This one's just a " + genders[gender] + ",\" Someone says.";
                UpdateMainText(text);
                yield return new WaitForSeconds(10);
                //conversation with leader
                GameObject.Find("DecisionManager").GetComponent<DecisionManager>().TalkTo(1000, "");
                convoM.StartConversation(6, 1009);
            }
        }
        else if (fireAtDeer == 1) {
            SpeechAlertObject.GetComponent<SpeechAlert>().convoID = -1;
            SpeechAlertObject.GetComponent<SpeechAlert>().characterID = 1003;
            SpeechAlertObject.GetComponent<SpeechAlert>().text = "That was way too close.";
            SpeechAlertObject.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Ben";
            SpeechAlertObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "That was way too close.";
            speechObject = Instantiate(SpeechAlertObject);
            speechAlertPanel.SetActive(true);
            speechObject.transform.SetParent(speechAlertPanel.transform, false);

            yield return new WaitForSeconds(5);

            SpeechAlertObject.GetComponent<SpeechAlert>().convoID = -1;
            SpeechAlertObject.GetComponent<SpeechAlert>().characterID = 1002;
            SpeechAlertObject.GetComponent<SpeechAlert>().text = "I agree, let's find Gregory.";
            SpeechAlertObject.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Akaysha";
            SpeechAlertObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "I agree, let's find Gregory.";
            speechObject = Instantiate(SpeechAlertObject);
            speechAlertPanel.SetActive(true);
            speechObject.transform.SetParent(speechAlertPanel.transform, false);
        }
    }

    public void StopCurrentMission() {
        StopCoroutine(CO);
    }


    public void UpdateMainText(string text) {

        Destroy(mainTextAreaPanel.transform.GetChild(0).gameObject);

        var mainText = Instantiate(mainTextObject);
        mainText.transform.GetChild(0).GetComponent<Text>().text = text;
        mainText.transform.SetParent(mainTextAreaPanel.transform, false);
    }
}
