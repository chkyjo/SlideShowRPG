using UnityEngine;

public class WaitAction : MonoBehaviour {

    string _textUpdate;

    public void OpenWaitPanel() {
        DecisionManager dM = GameObject.Find("DecisionManager").GetComponent<DecisionManager>();

        dM.OpenWaitPanel(_textUpdate);
    }

    public void SetTextUpdate(string text) {
        _textUpdate = text;
    }
    
    public string GetTextUpdate() {
        return _textUpdate;
    }
}
