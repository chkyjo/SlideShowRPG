using UnityEngine;

public class EatAction : MonoBehaviour {

    public void Eat(){
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().Eat();
    }
}
