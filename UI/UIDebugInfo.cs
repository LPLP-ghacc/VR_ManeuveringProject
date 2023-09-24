using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class UIDebugInfo : MonoBehaviour
{
    public GameObject player;
    public ActionBasedController lController, rController;

    public TextMeshProUGUI camField, leftHField, rightHField, playerpos, left, right;
    [Space]
    public Transform camPos, leftHandPos, rightHandPos;

    private string GetRounded(string startText, Transform targetPos)
    {
        string rounded = startText + $" - X: {Math.Round(targetPos.position.x, 2)}" +
            $", Y: {Math.Round(targetPos.position.y, 2)}," +
            $" Z: {Math.Round(targetPos.position.z, 2)}";

        return rounded;
    }

    private void Update()
    {
        camField.text = GetRounded("Camera Position", camPos);
        leftHField.text = GetRounded("Left Hand Position", leftHandPos);
        rightHField.text = GetRounded("Right Hand Position", rightHandPos);
        playerpos.text = $"Vector3: {player.transform.position}";

        Vector2 leftVelocity = new Vector2(lController.rotateAnchorAction.action.ReadValue<Vector2>().x, 
            lController.translateAnchorAction.action.ReadValue<Vector2>().y);

        left.text = $"LEFT G: {lController.selectAction.action.ReadValue<float>()} " +
            $"S: {lController.activateAction.action.ReadValue<float>()} " +
            $"velocity {leftVelocity}";
        right.text = $"right grab: {rController.selectAction.action.ReadValue<float>()}";
    }
}
