using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Shoot : MonoBehaviour
{
    public GameObject leftFirePoint;
    public GameObject rightFirePoint;
    public XRNode inputSource;

    public GameObject ammo;
    public GameObject electric;
    public GameObject electric2;
    public GameObject shield;

    
    
    private bool primaryPressed = false;

    private float nextShot;

    private bool isElectric;
    private GameObject gm;
    private VRMapping controlls;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager");
        controlls = gm.GetComponent<VRMapping>();
    }

    // Update is called once per frame
    void Update()
    {
        ///////////////////////left primary///////////////////
        if (controlls.leftPrimary || controlls.rightPrimary)
        {
            if (!primaryPressed)
            {
                primaryPressed = true;
                if (isElectric)
                {
                    isElectric = false;
                }
                else
                {
                    isElectric = true;
                }
            }

        }
        else
        {
            primaryPressed = false;
        }
        /////////////////////////////////////////////////////
        
        /////////////////////////right primary////////////////////
        if (controlls.leftSecondary || controlls.rightSecondary)
        {
            shield.SetActive(true);
        }
        else
        {
            shield.SetActive(false);
        }
        /////////////////////////////////////////////////////////////
        

        if (controlls.leftTrigger)
        {
            if (isElectric)
            {
                electric.SetActive(true);
                electric.transform.position = leftFirePoint.transform.position;
                electric.transform.eulerAngles = leftFirePoint.transform.parent.eulerAngles;

                electric2.SetActive(true);
                electric2.transform.position = rightFirePoint.transform.position;
                electric2.transform.eulerAngles = rightFirePoint.transform.parent.eulerAngles;
            }
            else
            {
                if (Time.time > nextShot)
                {

                    
                    Vector3 point = GetComponent<HeadTracking>().point;
                    var clone = Instantiate(ammo, leftFirePoint.transform.position, Quaternion.identity);
                    clone.GetComponent<Bullet>().direction = (point - leftFirePoint.transform.position).normalized;
                    nextShot = Time.time + 1;

                    var clone2 = Instantiate(ammo, rightFirePoint.transform.position, Quaternion.identity);
                    clone2.GetComponent<Bullet>().direction = (point - rightFirePoint.transform.position).normalized;
                    nextShot = Time.time + .3f;

                    Destroy(clone, 5);
                    Destroy(clone2, 5);
                    
                }
            }
            
            



        }
        else
        {
            electric.SetActive(false);
            electric2.SetActive(false);
        }

    }
}
