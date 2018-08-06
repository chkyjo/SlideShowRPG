using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Missions : MonoBehaviour {

    public int missionID;
    public int progressID;
    public GameObject conversationBackgroundPanel;
    public GameObject characterManager;
    public GameObject progressMission;
    public GameObject characterResponseObject;
    
    public GameObject exclaimationPanel;
    public GameObject exclaimationObject;
    public GameObject mainTextObject;
    public GameObject mainTextAreaPanel;

    public GameObject SpeechAlertObject;
    public GameObject speechAlertPanel;
    public GameObject exlamationAlertPanel;
    public int currentTime = 0;
    public int fireAtDeer = 1;

    public void UseMissionManager() {
        GameObject.Find("MissionManager").GetComponent<Missions>().StartMission(missionID);
    }

    public void StartMission(int ID) {
        switch (ID) {
            case 1:
                StartCoroutine(HuntingMission());
                break;
            case 2:
                break;
        }
    }

    public void SendMissionManagerNewProgress() {
        GameObject.Find("MissionManager").GetComponent<Missions>().UpdateProgress(progressID);
    }

    public void UpdateProgress(int progID) {
        progressID = progID;
    }

    IEnumerator HuntingMission() {

        CharactersManager cM = characterManager.GetComponent<CharactersManager>();

        string greeting = "Alright, let's meet in the woods on the other side of the archway.";
        StartCoroutine(GameObject.Find("ConversationManager").GetComponent<ConversationManager>().DisplayGreeting(1000, greeting));

        yield return new WaitForSeconds(3f);
        progressID = -1;

        if (conversationBackgroundPanel.active == true) {
            conversationBackgroundPanel.SetActive(false);
            GameObject.Find("ConversationManager").GetComponent<ConversationManager>().ClearConversation();
        }

        
        Character tempChar = cM.GetCharacter(1000);
        tempChar.SetLocation(6);
        tempChar = cM.GetCharacter(1001);
        tempChar.SetLocation(6);
        tempChar = cM.GetCharacter(1002);
        tempChar.SetLocation(6);
        tempChar = cM.GetCharacter(1003);
        tempChar.SetLocation(6);

        SettingManager sM = GameObject.Find("SettingManager").GetComponent<SettingManager>();
        GameObject decisionPanel = GameObject.Find("DecisionPanel");
        PlayerManager pM = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        ConversationManager convoM = GameObject.Find("ConversationManager").GetComponent<ConversationManager>();

        bool notArrived = true;

        while (notArrived) {

            if (sM.currentRoom == 6 && cM.GetCharacter(1000).GetLocation() == 6) {
                greeting = "Alright, finally. " + pM.GetName() + ", you go with Akaysha and Ben. Jasmine and I will take left. Create a perimeter of about 1000 feet. Move it!.";
                GameObject.Find("DecisionManager").GetComponent<DecisionManager>().TalkTo(1000, greeting);
                cM.GetCharacter(1000).SetLocation(9);
                cM.GetCharacter(1001).SetLocation(9);

                notArrived = false;
            }

            yield return new WaitForSeconds(.5f);
        }

        progressMission.transform.GetChild(2).GetComponent<Text>().text = "Create perimeter";
        progressMission.GetComponent<Missions>().progressID = 0;
        var obj = Instantiate(progressMission);
        obj.transform.SetParent(decisionPanel.transform.GetChild(0).GetChild(0).transform, false);

        bool startedPerimeter = false;

        string text;

        while (startedPerimeter == false) {
            if (progressID == 0) {
                startedPerimeter = true;
                cM.GetCharacter(1002).SetLocation(8);
                cM.GetCharacter(1003).SetLocation(8);
                text = "You and your companions carefully patrol left, creating a perimeter of 1000 feet. All you can see are trees in all directions. You have not even seen a bit of wildlife. The ground was barron and claimed by only the roots of the tightly packed trees.";
                GameObject.Find("DecisionManager").GetComponent<DecisionManager>().NextRoom(8, text);
            }
            yield return new WaitForSeconds(.5f);
        }

        progressID = 1;
        yield return new WaitForSeconds(15f);

        SpeechAlertObject.GetComponent<SpeechAlertManager>().convoID = 4;
        SpeechAlertObject.GetComponent<SpeechAlertManager>().characterID = 1003;
        SpeechAlertObject.GetComponent<SpeechAlertManager>().text = "I'm scared...";
        SpeechAlertObject.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Ben";
        SpeechAlertObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "I'm scared...";
        var speechObject = Instantiate(SpeechAlertObject);
        speechAlertPanel.SetActive(true);
        speechObject.transform.SetParent(speechAlertPanel.transform, false);


        cM.GetCharacter(1002).SetGreeting("What...");
        cM.GetCharacter(1003).SetGreeting("What...");

        yield return new WaitForSeconds(20f);

        SpeechAlertObject.GetComponent<SpeechAlertManager>().convoID = 2;
        SpeechAlertObject.GetComponent<SpeechAlertManager>().characterID = 1002;
        SpeechAlertObject.GetComponent<SpeechAlertManager>().text = "...";
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

        SpeechAlertObject.GetComponent<SpeechAlertManager>().convoID = 5;
        SpeechAlertObject.GetComponent<SpeechAlertManager>().characterID = 1002;
        SpeechAlertObject.GetComponent<SpeechAlertManager>().text = "I'm gonna take the shot.";
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
                if(currentTime == 3) {
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
            SpeechAlertObject.GetComponent<SpeechAlertManager>().convoID = -1;
            SpeechAlertObject.GetComponent<SpeechAlertManager>().characterID = 1002;
            SpeechAlertObject.GetComponent<SpeechAlertManager>().text = "Let's get out of here then.";
            SpeechAlertObject.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Akaysha";
            SpeechAlertObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Let's get out of here then.";
            speechObject = Instantiate(SpeechAlertObject);
            speechAlertPanel.SetActive(true);
            speechObject.transform.SetParent(speechAlertPanel.transform, false);

            yield return new WaitForSeconds(3);

            text = "A little more cautious now you and your group start heading back the way you came.";
            UpdateMainText(text);

        }

        if(sM.combat == 1) {
            text = "An arrow wizzes by your neck and hits Akaysha in the back. You can see the pointy end protrude out her chest as she spins and falls. Another sickening sound as a second arrow hits Ben in the neck.";
            UpdateMainText(text);
            cM.GetCharacter(1002).SetStatus("Dead");
            cM.GetCharacter(1003).SetStatus("Dead");

            for(int i = 0; i < 10; i++) {
                yield return new WaitForSeconds(1f);
            }

            if(sM.combat == 1) {
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
        else if(fireAtDeer == 1){
            SpeechAlertObject.GetComponent<SpeechAlertManager>().convoID = -1;
            SpeechAlertObject.GetComponent<SpeechAlertManager>().characterID = 1003;
            SpeechAlertObject.GetComponent<SpeechAlertManager>().text = "That was way too close.";
            SpeechAlertObject.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Ben";
            SpeechAlertObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "That was way too close.";
            speechObject = Instantiate(SpeechAlertObject);
            speechAlertPanel.SetActive(true);
            speechObject.transform.SetParent(speechAlertPanel.transform, false);

            yield return new WaitForSeconds(5);

            SpeechAlertObject.GetComponent<SpeechAlertManager>().convoID = -1;
            SpeechAlertObject.GetComponent<SpeechAlertManager>().characterID = 1002;
            SpeechAlertObject.GetComponent<SpeechAlertManager>().text = "I agree, let's find Gregory.";
            SpeechAlertObject.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Akaysha";
            SpeechAlertObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "I agree, let's find Gregory.";
            speechObject = Instantiate(SpeechAlertObject);
            speechAlertPanel.SetActive(true);
            speechObject.transform.SetParent(speechAlertPanel.transform, false);
        }
        
    }

    public void UpdateMainText(string text) {

        Destroy(mainTextAreaPanel.transform.GetChild(0).gameObject);

        var mainText = Instantiate(mainTextObject);
        mainText.transform.GetChild(0).GetComponent<Text>().text = text;
        mainText.transform.SetParent(mainTextAreaPanel.transform, false);
    }
}
