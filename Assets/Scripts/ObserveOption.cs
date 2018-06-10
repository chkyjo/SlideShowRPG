using UnityEngine;

public class ObserveOption : MonoBehaviour {

    public void Observe(){
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().ObserveSurroundings();
    }
}
