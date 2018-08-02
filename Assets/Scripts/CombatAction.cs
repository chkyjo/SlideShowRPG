using UnityEngine;

public class CombatAction : MonoBehaviour {

    public int combatActionID;
    public string displayText;

    public void DoCombatAction(){
        DecisionManager dM = GameObject.Find("DecisionManager").GetComponent<DecisionManager>();
        if(combatActionID == 1) {
            dM.Attack();
        }else if(combatActionID == 2) {
            dM.Throw();
        }
        else if(combatActionID == 3) {
            dM.Retreat();
        }
    }
}
