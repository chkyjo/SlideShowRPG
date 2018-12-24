using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviorManager : MonoBehaviour {

    string[] feelingsTowardPlayer;
    string[] feelingsForAllies;
    string[] feelingsForFamily;

    void Start() {
        feelingsTowardPlayer = new string[21] { "death", "death", "dispise", "dispise",
            "hate", "hate", "dislike", "dislike", "tolerate", "tolerate", "base",
            "aquiantance", "aquaintance", "friendly", "friend", "goodFriend", "admires",
            "admires", "love", "devoted", "devoted" };

        feelingsForAllies = new string[21] {"death", "dispise", "dispise",
            "hate", "hate", "dislike", "dislike", "tolerate", "tolerate",
            "tolerate", "base", "friendly", "friendly", "friend", "goodFriend", "admires",
            "admires", "love", "love", "devoted", "devoted"};

        feelingsForFamily = new string[21] {"hate", "dislike", "dislike",
            "dislike", "tolerate", "tolerate", "tolerate", "like", "like",
            "like", "love", "love", "love", "love", "love", "love",
            "love", "love", "devoted", "devoted", "devoted"};
    }

    public string GetCharacterFeelingsTowardsPlayer(Character character) {
        string relationship = character.GetRelationship();
        if(relationship == "None") {
            return feelingsTowardPlayer[character.GetRelationshipLvl() / 5];
        }
        else if(relationship == "Ally") {
            return feelingsForAllies[character.GetRelationshipLvl() / 5];
        }
        else {
            return feelingsForFamily[character.GetRelationshipLvl() / 5];
        }
    }

}
