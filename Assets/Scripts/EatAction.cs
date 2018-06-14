using UnityEngine;

public class EatAction : MonoBehaviour {

    public int ID;

    public void Eat(){
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().Eat();
    }
}
