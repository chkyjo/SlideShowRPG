using UnityEngine;

public class CancelDecisionButtonScript : MonoBehaviour {

    public void CancelDecision() {
        GameObject.FindWithTag("DecisionManager").GetComponent<DecisionManager>().RefreshDecisionList();
    }
}
