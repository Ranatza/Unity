using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class VRMapping : MonoBehaviour
{
    public XRNode leftHand;
    public XRNode rightHand;

    public GameObject leftModel;
    public GameObject rightModel;
    private Animator leftAnim;
    private Animator rightAnim;

    public Vector2 leftStick;
    public float leftTrigger;
    public float leftGrip;
    public bool leftPrimary;
    public bool leftSecondary;

    public Vector2 rightStick;
    public float rightTrigger;
    public float rightGrip;
    public bool rightPrimary;
    public bool rightSecondary;

    private InputDevice leftController;
    private InputDevice rightController;


    // Start is called before the first frame update
    void Start()
    {
        leftAnim = leftModel.GetComponent<Animator>();
        rightAnim = rightModel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        leftController = InputDevices.GetDeviceAtXRNode(leftHand);
        rightController = InputDevices.GetDeviceAtXRNode(rightHand);

        leftAnim.SetFloat("Trigger", leftTrigger);
        leftAnim.SetFloat("Grip", leftGrip);
        leftAnim.SetFloat("Joy X", leftStick.x);
        leftAnim.SetFloat("Joy Y", leftStick.y);
        if (leftPrimary)
        {
            leftAnim.SetFloat("Button 1", 1);
        }
        else
        {
            leftAnim.SetFloat("Button 1", 0);
        }

        if (leftSecondary)
        {
            leftAnim.SetFloat("Button 2", 1);
        }
        else
        {
            leftAnim.SetFloat("Button 2", 0);
        }

        rightAnim.SetFloat("Trigger", rightTrigger);
        rightAnim.SetFloat("Grip", rightGrip);
        rightAnim.SetFloat("Joy X", rightStick.x);
        rightAnim.SetFloat("Joy Y", rightStick.y);
        if (rightPrimary)
        {
            rightAnim.SetFloat("Button 1", 1);
        }
        else
        {
            rightAnim.SetFloat("Button 1", 0);
        }

        if (rightSecondary)
        {
            rightAnim.SetFloat("Button 2", 1);
        }
        else
        {
            rightAnim.SetFloat("Button 2", 0);
        }



        leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out leftStick);
        leftController.TryGetFeatureValue(CommonUsages.trigger, out leftTrigger);
        leftController.TryGetFeatureValue(CommonUsages.grip, out leftGrip);
        leftController.TryGetFeatureValue(CommonUsages.primaryButton, out leftPrimary);
        leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out leftSecondary);

        rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out rightStick);
        rightController.TryGetFeatureValue(CommonUsages.trigger, out rightTrigger);
        rightController.TryGetFeatureValue(CommonUsages.grip, out rightGrip);
        rightController.TryGetFeatureValue(CommonUsages.primaryButton, out rightPrimary);
        rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out rightSecondary);

    }
}
