using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSelected : MonoBehaviour
{
    public Text infoText;
    Machine machine;

    private void Start()
    {
        machine = GameObject.Find("Machine").GetComponent<Machine>();
    }

    void Update()
    {
        if (machine.medPackRecipe && !machine.largePackRecipe)
        {
            infoText.text = "Crafting: Medium Packages";
        }
        else if (machine.largePackRecipe && !machine.medPackRecipe)
        {
            infoText.text = "Crafting: Large Packages";
        }
        else if (!machine.medPackRecipe && !machine.largePackRecipe)
        {
            infoText.text = "Crafting: Nothing";
        }
    }
}
