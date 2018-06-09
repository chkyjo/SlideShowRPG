using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoomAction : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextRoom()
    {
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().NextRoom();
    }

}