using UnityEngine;

public class ChanceAction : MonoBehaviour {

    int _actionID;

    public void CallFunction() {
        
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().PerformChanceAction(_actionID);
        Destroy(gameObject);
    }

    public void SetActionID(int ID) {
        _actionID = ID;
    }

    public int GetActionID() {
        return _actionID;
    }
}
