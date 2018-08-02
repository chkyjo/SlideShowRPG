using UnityEngine;
using UnityEngine.UI;

public class CategoryManager : MonoBehaviour {

    public int categoryToOpenID;
    public string displayText;


	public void OpenCategory() {
        DecisionManager dM = GameObject.Find("DecisionManager").GetComponent<DecisionManager>();
        if (categoryToOpenID == 1) {
            dM.DisplayExitOptions();

        }else if(categoryToOpenID == 2) {
            dM.DisplayCombatOptions();
        }
        else {

        }
    }

}
