using UnityEngine;
using UnityEngine.UI;

public class AttackAction : MonoBehaviour {

    public int characterID;
    public Slider healthBar;
    public Button attackButton;
    public Text resultText;
    
    //called after selecting attack option
    public void AttackDecision() {
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().Attack();
    }
    //called after selecting person to attack
    public void AttackPerson() {
        int[] IDs = new int[1] { characterID };
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().AttackPeople(IDs);
    }
    //called when pressing attack
    public void Attack(){
        bool result;
        result = GameObject.Find("DecisionManager").GetComponent<DecisionManager>().AttackAction(characterID);
        Character tempChar = GameObject.Find("CharacterManager").GetComponent<CharactersManager>().GetCharacter(characterID);
        healthBar.value = tempChar.GetHealth();
        if(healthBar.value <= 0) {
            healthBar.value = 0;
            attackButton.interactable = false;
        }
        if (result) {
            resultText.text = "You did 43 damage!";
        }
        else {
            resultText.text = "Your attack misses!";
            
        }
    }
}
