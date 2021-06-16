using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeltPower : MonoBehaviour
{
    public Text infoText;

    ConveyorBelt2 beltScript;

    private void Start()
    {
        beltScript = GameObject.Find("Conveyor Belt Straight").GetComponent<ConveyorBelt2>();
    }

    void Update()
    {
        if (beltScript.BeltOn)
        {
            infoText.text = "ON";
        }
        else
        {
            infoText.text = "OFF";
        }
    }
}
