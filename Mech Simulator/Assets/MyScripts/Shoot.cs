using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Shoot : MonoBehaviour
{
    public GameObject[] guns;
    public XRNode inputSource;

    public GameObject ammo;
    public GameObject electric;
    public GameObject electric2;
    public GameObject shield;

    public GameObject[] aimPoint;
    private InputDevice device;
    private bool trigger;
    private bool primary;
    private bool primaryPressed = false;
    private bool secondary;

    private float fireDelay;
    private float nextShot;

    private bool isElectric;

    // Start is called before the first frame update
    void Start()
    {
        //aimPoint[0] = guns[0].transform.GetChild(0).gameObject;
        //aimPoint[1] = guns[1].transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.triggerButton, out trigger);
        device.TryGetFeatureValue(CommonUsages.primaryButton, out primary);
        device.TryGetFeatureValue(CommonUsages.secondaryButton, out secondary);
        if (primary)
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

        if (secondary)
        {
            shield.SetActive(true);
        }
        else
        {
            shield.SetActive(false);
        }

        if (trigger)
        {
            if (isElectric)
            {
                electric.SetActive(true);
                electric.transform.position = guns[0].transform.position;
                electric.transform.eulerAngles = guns[0].transform.parent.eulerAngles;

                electric2.SetActive(true);
                electric2.transform.position = guns[1].transform.position;
                electric2.transform.eulerAngles = guns[1].transform.parent.eulerAngles;
            }
            else
            {
                if (Time.time > nextShot)
                {

                    
                    Vector3 point = GetComponent<HeadTracking>().point;
                    var clone = Instantiate(ammo, guns[0].transform.position, Quaternion.identity);
                    clone.GetComponent<Bullet>().direction = (point - guns[0].transform.position).normalized;
                    nextShot = Time.time + 1;

                    var clone2 = Instantiate(ammo, guns[1].transform.position, Quaternion.identity);
                    clone2.GetComponent<Bullet>().direction = (point - guns[1].transform.position).normalized;
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
