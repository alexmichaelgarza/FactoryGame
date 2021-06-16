using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{   
    Rigidbody rb;
    Camera cam;
    int maxGrabDistance;
    float selectionDistance;

    //public GameObject smallPackage;
    //public GameObject mediumPackage;
    //public GameObject largePackage;
    //public GameObject straightBeltBP;
    //public GameObject ControlPanel;

    void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
        maxGrabDistance = 3;
    }

    void FixedUpdate()
    {       

        if (rb)
        {
            if (!rb.isKinematic)
            {
                rb.isKinematic = true;
            }

            rb.MovePosition(cam.ScreenToWorldPoint(Input.mousePosition) + (cam.transform.forward * selectionDistance));                       
        }
    }    

    void Update()
    {
        if (!cam)
        {
            return;
        }

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    SpawnItem(smallPackage);
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    SpawnItem(mediumPackage);
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    SpawnItem(largePackage);
        //}

        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    SpawnItem(straightBeltBP);
        //}

        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    SpawnItem(ControlPanel);
        //}

        //Grab/Drop
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, maxGrabDistance))
            {
                Interactables interactables = hitInfo.collider.GetComponent<Interactables>();

                if (interactables != null)
                {
                    if (rb)
                    {
                        rb.freezeRotation = false;
                        rb.isKinematic = false;
                        rb = null;
                    }
                    else
                    {
                        rb = GetRigidbodyFromMouseClick();
                    }
                }                
            }
        }

        //if (Input.GetKey(KeyCode.X))
        //{
        //    DestroyObject();
        //}             
    }   

    //private void SpawnItem(GameObject gameObject)
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;
    //    float InteractDistance = 3f;

    //    if (Physics.Raycast(ray, out hit, InteractDistance))
    //    {
    //        float y = hit.point.y + (gameObject.transform.localScale.y / 2f);
    //        Vector3 pos = new Vector3(hit.point.x, y, hit.point.z);
    //        gameObject.transform.position = pos;
    //        Instantiate(gameObject);
    //    }
    //    else
    //    {
    //        Debug.Log("Too Far");
    //    }
    //}   

    //private void DestroyObject()
    //{
    //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hitInfo = new RaycastHit();      

    //    if (Physics.Raycast(ray, out hitInfo, 10f))
    //    {
    //        if (hitInfo.collider.gameObject.GetComponent<Rigidbody>())
    //        {
    //            Destroy(hitInfo.collider.gameObject);
    //        }
    //    }
    //}

    Rigidbody GetRigidbodyFromMouseClick()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, maxGrabDistance))
        {
            if (hitInfo.collider.gameObject.GetComponent<Rigidbody>())
            {
                Rigidbody hitRB = hitInfo.collider.gameObject.GetComponent<Rigidbody>();
                hitRB.freezeRotation = true;
                selectionDistance = Vector3.Distance(ray.origin, hitInfo.point);
                return hitInfo.collider.gameObject.GetComponent<Rigidbody>();
            }
        }
        return null;
    }
}
