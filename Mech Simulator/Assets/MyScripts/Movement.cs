using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Movement : MonoBehaviour
{
    public float speed;
    public XRNode inputSource;
    public XRNode inputSource2;

    private XRRig rig;
    private Vector2 inputAxis;
    private Vector2 rightInputAxis;
    private CharacterController character;
    public bool trigger;

    private InputDevice device;
    private InputDevice device2;



    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Mech").GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
    }

    // Update is called once per frame
    void Update()
    {
        device = InputDevices.GetDeviceAtXRNode(inputSource);
        device2 = InputDevices.GetDeviceAtXRNode(inputSource2);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
        device.TryGetFeatureValue(CommonUsages.triggerButton, out trigger);
        device2.TryGetFeatureValue(CommonUsages.primary2DAxis, out rightInputAxis);
    }

    private void FixedUpdate()
    {
        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        character.Move(direction * Time.fixedDeltaTime * speed);
        if(rightInputAxis.x > 0)
        {
            Debug.Log("turn");
            character.transform.Rotate(0, .3f, 0);
        }
        if (rightInputAxis.x < 0)
        {
            Debug.Log("turn");
            character.transform.Rotate(0, -.3f, 0);
        }




    }
}
