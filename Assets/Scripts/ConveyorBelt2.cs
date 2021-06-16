using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt2 : MonoBehaviour
{
    public GameObject belt;
    Camera cam;
    public Transform endpoint;
    public float CurrentSpeed;
    public float MaxSpeed;

    GameObject PowerSwitch;
    GameObject SpeedSwitch;
    public GameObject ButtonHit;    
    public bool BeltOn;

    private void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
        PowerSwitch = GameObject.FindGameObjectWithTag("PowerSwitch");
        SpeedSwitch = GameObject.FindGameObjectWithTag("SpeedSwitch");
        ButtonHit = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckForButton())
            {
                if (ButtonHit == PowerSwitch)
                {
                    Power();

                }

                if (ButtonHit == SpeedSwitch)
                {
                    Speed();
                }
            }            
        }        
    }

    void Speed()
    {        
        if (CurrentSpeed >= MaxSpeed)
        {
            CurrentSpeed = 0;
        }
        else
        {
            CurrentSpeed += .5f;
        }
    }

    void Power()
    {        
        if (BeltOn)
        {
            BeltOn = false;
        }
        else
        {
            BeltOn = true;
        }
    }

    bool CheckForButton()
    {
        float rayDistance = 2f;
        bool RayHitButton = false;

        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit rayHit, rayDistance))
        {
            GameObject HitObject = rayHit.collider.gameObject;
            if (HitObject == PowerSwitch)
            {
                RayHitButton = true;

                ButtonHit = PowerSwitch;                
            }

            if (HitObject == SpeedSwitch)
            {
                RayHitButton = true;

                ButtonHit = SpeedSwitch;
            }
        }
        return RayHitButton;      
    }

    void OnTriggerStay(Collider other)
    {
        if (BeltOn)
        {
            other.transform.position = Vector3.MoveTowards(other.transform.position, endpoint.position, CurrentSpeed * Time.deltaTime);            
        }     
    }
}
