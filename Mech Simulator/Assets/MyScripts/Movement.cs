using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Movement : MonoBehaviour
{
    public float speed;
    public GameObject xrRig;
    
    private XRRig rig;
    private CharacterController character;

    private GameObject gm;
    private VRMapping controlls;



    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager");
        controlls = gm.GetComponent<VRMapping>();
        character = GameObject.Find("Mech").GetComponent<CharacterController>();
        rig = xrRig.GetComponent<XRRig>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(controlls.leftStick.x, 0, controlls.leftStick.y);

        character.Move(direction * Time.fixedDeltaTime * speed);
        if(controlls.rightStick.x > 0)
        {
            Debug.Log("turn");
            character.transform.Rotate(0, .3f, 0);
        }
        if (controlls.rightStick.x < 0)
        {
            Debug.Log("turn");
            character.transform.Rotate(0, -.3f, 0);
        }




    }
}
