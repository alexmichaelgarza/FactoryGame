using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachinePower : MonoBehaviour
{
    public Text infoText;
    Machine machine;

    private void Start()
    {
        machine = GameObject.Find("Machine").GetComponent<Machine>();
    }

    void Update()
    {
        if (machine.IsMachineOn)
        {
            infoText.text = "Machine: ON";
        }
        else
        {
            infoText.text = "Machine: OFF";
        }        
    }
}
