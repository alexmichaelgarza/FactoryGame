using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public int SmallPackageCount;
    public int MediumPackageCount;

    public GameObject smallPackageOutput;
    public GameObject medPackageOutput;
    public GameObject largePackageOutput;
    public Transform SpawnPoint;

    public bool medPackRecipe;
    public bool largePackRecipe;
    public bool isMaterials;
    public bool IsMachineOn;
    float OutputSpeed;
    public int outputSpeedLevel;

    //private int buildTick;
    //private int buildTickMAX;
    //private bool isCrafting;

    private void Awake()
    {
        SmallPackageCount = 0;
        MediumPackageCount = 0;
        OutputSpeed = 1f;
        outputSpeedLevel = 5;
        IsMachineOn = false;
        isMaterials = false;
        medPackRecipe = false;
        largePackRecipe = false;
    }    

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("SmallPackage"))
        {
            SmallPackageCount++;
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.CompareTag("MedPackage"))
        {
            MediumPackageCount++;
            Destroy(collider.gameObject);
        }
        else
        {
            return;
        }
    }    

    private void Update()
    {
        #region Machine Keyboard Control

        //Turn Machine On/Off
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (IsMachineOn)
            {
                CancelInvokes();
                IsMachineOn = false;
            }
            else
            {
                IsMachineOn = true;
            }
        }

        //Change Machine Output Speed
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            IncreaseOutSpeed();            
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            DecreaseOutSpeed();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            OutputSpeed = 1f;
            outputSpeedLevel = 5;
        }

        //Choose Crafting Recipe
        if (Input.GetKeyDown(KeyCode.Alpha7) && IsMachineOn)
        {
            if (medPackRecipe)
            {
                CancelInvokes();
                medPackRecipe = false;
            }
            else
            {
                if (largePackRecipe)
                {
                    largePackRecipe = false;
                }
                medPackRecipe = true;
                InvokeRepeating(nameof(MedPackageRecipe), OutputSpeed, OutputSpeed);                
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha8) && IsMachineOn)
        {
            if (largePackRecipe)
            {
                CancelInvokes();
                largePackRecipe = false;
            }
            else
            {
                if (medPackRecipe)
                {
                    medPackRecipe = false;
                }
                largePackRecipe = true;
                InvokeRepeating(nameof(LargePackageRecipe), OutputSpeed, OutputSpeed);
            }
        }

        //if (medPackRecipe)
        //{
        //    CraftPackage(SmallPackageCount, medPackageOutput);            
        //}
        //if (largePackRecipe)
        //{
        //    CraftPackage(MediumPackageCount, largePackageOutput);            
        //}
        #endregion
    }

    #region Speed Change Functions
    private void IncreaseOutSpeed()
    {
        if (IsMachineOn && outputSpeedLevel <= 9)
        {
            OutputSpeed /= 1.1f;
            OutputSpeed = (Mathf.Round(OutputSpeed * 10)) / 10.0f;
            Mathf.Clamp(OutputSpeed, 0.5f, 1.5f);
            Mathf.Clamp(outputSpeedLevel, 1, 10);
            outputSpeedLevel++;
        }
        else
        {
            return;
        }
    }

    private void DecreaseOutSpeed()
    {
        if (IsMachineOn && outputSpeedLevel >= 1)
        {
            OutputSpeed *= 1.1f;
            OutputSpeed = (Mathf.Round(OutputSpeed * 10)) / 10.0f;
            Mathf.Clamp(OutputSpeed, 0.5f, 1.5f);
            Mathf.Clamp(outputSpeedLevel, 1, 10);
            outputSpeedLevel--;
        }
        else
        {
            return;
        }
    }
    #endregion

    #region Recipes
    private void MedPackageRecipe()
    {
        if (SmallPackageCount < 2)
        {
            isMaterials = false;
        }
        else if (SmallPackageCount >= 2)
        {
            Instantiate(medPackageOutput, SpawnPoint.position, transform.rotation);
            SmallPackageCount -= 2;
        }
    }

    private void LargePackageRecipe()
    {
        if (MediumPackageCount < 2)
        {
            isMaterials = false;
        }
        else if (MediumPackageCount >= 2)
        {
            Instantiate(largePackageOutput, SpawnPoint.position, transform.rotation);
            MediumPackageCount -= 2;            
        }
    }
    #endregion

    private void CancelInvokes()
    {
        CancelInvoke(nameof(MedPackageRecipe));
        CancelInvoke(nameof(LargePackageRecipe));
        CancelInvoke(nameof(IncreaseOutSpeed));
        CancelInvoke(nameof(DecreaseOutSpeed));
        //CancelInvoke("CraftPackage");
        medPackRecipe = false;
        largePackRecipe = false;
    }

    //private void CraftPackage(int packageCount, GameObject packageOutput)
    //{
    //    if (packageCount < 2)
    //    {
    //        Debug.Log("Not Enough Materials");
    //    }
    //    else if (packageCount >= 2)
    //    {
    //        Instantiate(packageOutput, SpawnPoint.position, packageOutput.transform.rotation);
    //        packageCount -= 2;
    //    }
    //}
}
