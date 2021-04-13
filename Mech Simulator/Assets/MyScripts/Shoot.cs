using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class Shoot : MonoBehaviour
{
    public TextMeshProUGUI pAmmoDisplay;

    public GameObject leftFirePoint;
    public GameObject rightFirePoint;
    public XRNode inputSource;

    public GameObject ammo;
    public GameObject electric;
    public GameObject electric2;
    public GameObject shield;

    public GameObject plasmaScreen;
    public GameObject lightningScreen;

    public Material normalMat;
    public Material selectedMat;

    private int maxPlasmaAmmo = 10;
    private int maxElectricEnergy;

    private int plasmaAmmo;
    private int electricEnergy;

    private float nextPlasmaRecharge;
    private float nextEnergyRecharge;
    
    private bool primaryPressed = false;

    private float nextShot;

    private bool isElectric;
    private GameObject gm;
    private VRMapping controlls;

    // Start is called before the first frame update
    void Start()
    {
        plasmaAmmo = maxPlasmaAmmo;
        electricEnergy = maxElectricEnergy;
        gm = GameObject.Find("GameManager");
        controlls = gm.GetComponent<VRMapping>();
    }

    // Update is called once per frame
    void Update()
    {
        rechargeAmmo();
        ///////////////////////left primary///////////////////
        if (controlls.leftPrimary || controlls.rightPrimary)
        {
            if (!primaryPressed)
            {
                primaryPressed = true;
                if (isElectric)
                {
                    isElectric = false;
                    setPlasma();
                }
                else
                {
                    isElectric = true;
                    setElectric();
                    

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
        

        if (controlls.leftTrigger > .2f || controlls.rightTrigger > .2f)
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
                if (Time.time > nextShot && plasmaAmmo > 0)
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
                    plasmaAmmo--;
                    
                }
            }


            


        }
        else
        {
            electric.SetActive(false);
            electric2.SetActive(false);
        }

    }

    private void setElectric()
    {
        foreach (Transform child in lightningScreen.transform)
        {
            if (child.gameObject.name == "Trim")
            {
                child.GetComponent<Renderer>().material = selectedMat;
            }
        }

        foreach (Transform child in plasmaScreen.transform)
        {
            if (child.gameObject.name == "Trim")
            {
                child.GetComponent<Renderer>().material = normalMat;
            }
        }
    }

    private void setPlasma()
    {
        foreach (Transform child in lightningScreen.transform)
        {
            if (child.gameObject.name == "Trim")
            {
                child.GetComponent<Renderer>().material = normalMat;
            }
        }

        foreach (Transform child in plasmaScreen.transform)
        {
            if (child.gameObject.name == "Trim")
            {
                child.GetComponent<Renderer>().material = selectedMat;
            }
        }
    }

    private void rechargeAmmo()
    {

        if (isElectric)
        {
            if (Time.time > nextPlasmaRecharge)
            {
                if (plasmaAmmo < maxPlasmaAmmo)
                {
                    plasmaAmmo++;
                }
                nextPlasmaRecharge = Time.time + 2;
            }
        }
        else
        {
            
        }

        pAmmoDisplay.text = plasmaAmmo + "/ 10";
    }
}
