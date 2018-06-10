using UnityEngine;

public class TalkOption : MonoBehaviour {

    public void Talk(){
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().Talk();
    }
}
