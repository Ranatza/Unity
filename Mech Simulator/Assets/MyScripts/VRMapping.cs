using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class VRMapping : MonoBehaviour
{
    public XRNode leftHand;
    public XRNode rightHand;

    public Vector2 leftStick;
    public bool leftTrigger;
    public bool leftGrip;
    public bool leftPrimary;
    public bool leftSecondary;

    public Vector2 rightStick;
    public bool rightTrigger;
    public bool rightGrip;
    public bool rightPrimary;
    public bool rightSecondary;

    private InputDevice leftController;
    private InputDevice rightController;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        leftController = InputDevices.GetDeviceAtXRNode(leftHand);
        rightController = InputDevices.GetDeviceAtXRNode(rightHand);

        leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out leftStick);
        leftController.TryGetFeatureValue(CommonUsages.triggerButton, out leftTrigger);
        leftController.TryGetFeatureValue(CommonUsages.gripButton, out leftGrip);
        leftController.TryGetFeatureValue(CommonUsages.primaryButton, out leftPrimary);
        leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out leftSecondary);

        rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out rightStick);
        rightController.TryGetFeatureValue(CommonUsages.triggerButton, out rightTrigger);
        rightController.TryGetFeatureValue(CommonUsages.gripButton, out rightGrip);
        rightController.TryGetFeatureValue(CommonUsages.primaryButton, out rightPrimary);
        rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out rightSecondary);

    }
}
