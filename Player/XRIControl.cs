using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.XR.Interaction.Toolkit;

public class XRIControl : MonoBehaviour
{
    public ActionBasedController leftController, rightController;

    /// <summary>
    /// Events triggered when controls are clicked
    /// </summary>
    public static Action
        onTriggerLeftController,
        onTriggerRightController,
        onGrabLeftController,
        onGrabRightController;

    /// <summary>
    /// Value can be changed from 0 to 1
    /// </summary>
    public float
        triggerLeftController,
        triggerRightController,
        grabLeftController,
        grabRightController;

    /// <summary>
    /// Direction of movement
    /// </summary>
    public Vector2 leftControllerDirection = Vector2.zero;
    public Vector2 rightControllerDirection = Vector2.zero;

    private void Update()
    {
        leftControllerDirection = new Vector2(leftController.rotateAnchorAction.action.ReadValue<Vector2>().x, 
            leftController.translateAnchorAction.action.ReadValue<Vector2>().y);

        rightControllerDirection = new Vector2(rightController.rotateAnchorAction.action.ReadValue<Vector2>().x, 
            rightController.translateAnchorAction.action.ReadValue<Vector2>().y);

        triggerLeftController = leftController.activateAction.action.ReadValue<float>();
        triggerRightController = rightController.activateAction.action.ReadValue<float>();
        grabLeftController = leftController.selectAction.action.ReadValue<float>();
        grabRightController = rightController.selectAction.action.ReadValue<float>();

        if (triggerLeftController == 1) onTriggerLeftController?.Invoke();
        if (triggerRightController == 1) onTriggerRightController?.Invoke();
        if (grabLeftController == 1) onGrabLeftController?.Invoke();
        if (grabRightController == 1) onGrabRightController?.Invoke();
    }
    //лишь одна строчка кода может вызвать дерпессию ЕБАНУЮ
}
