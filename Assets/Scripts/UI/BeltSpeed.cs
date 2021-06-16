using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeltSpeed : MonoBehaviour
{
    public Text infoText;

    ConveyorBelt2 beltScript;

    private void Start()
    {
        beltScript = GameObject.Find("Conveyor Belt Straight").GetComponent<ConveyorBelt2>();
    }

    void Update()
    {
        infoText.text = beltScript.CurrentSpeed.ToString();
    }
}
