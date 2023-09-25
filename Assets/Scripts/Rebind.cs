using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Rebind : MonoBehaviour
{
    [SerializeField] private InputActionReference actionToRemap;
    [SerializeField] private TextMeshProUGUI buttonText;
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    public void StartRebinding()
    {
        EventSystem.current.SetSelectedGameObject(null);

        buttonText.text = "PRESS ANY BUTTON";
        actionToRemap.action.Disable();
        rebindingOperation = actionToRemap.action.PerformInteractiveRebinding().OnMatchWaitForAnother(0.1f).OnComplete(operation =>
        {
            buttonText.text = InputControlPath.ToHumanReadableString(actionToRemap.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
            rebindingOperation.Dispose();
            actionToRemap.action.Enable();


        }).Start();
    }
}
