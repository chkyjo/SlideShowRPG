using UnityEngine;

public class PickUpAction : MonoBehaviour {

	public void ListItemsToPickUp() {
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().ListItemsToPickUp();
    }
}
