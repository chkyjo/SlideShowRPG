using UnityEngine;

public class WaitInSeating : MonoBehaviour {
	public void WaitInSeat(){
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().WaitInSeating();
    }
}
