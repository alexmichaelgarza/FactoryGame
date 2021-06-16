using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutputSpeed : MonoBehaviour
{
    public Text infoText;
    Machine machine;

    private void Start()
    {
        machine = GameObject.Find("Machine").GetComponent<Machine>();
    }

    void Update()
    {
        infoText.text = "Output Speed: " + machine.outputSpeedLevel;
    }
}
