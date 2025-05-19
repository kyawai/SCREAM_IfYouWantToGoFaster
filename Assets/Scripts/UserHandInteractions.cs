using System;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// Player hand animation controller
/// to control when the user presses grip or trigger button
/// and animate it appropriately
/// </summary>
public class UserHandInteractions : MonoBehaviour
{
    #region Button references
    [Header("Left Button References")]
    [Tooltip("Left grip button reference")]
    [SerializeField]
    private InputActionReference _leftGripButton;
    [Tooltip("Left trigger button reference")]
    [SerializeField]
    private InputActionReference _leftTriggerButton;

    [Header("Right Button References")]
    [Tooltip("Right grip button reference")]
    [SerializeField]
    private InputActionReference _rightGripButton;
    [Tooltip("Right trigger button reference")]
    [SerializeField]
    private InputActionReference _rightTriggerButton;

    #endregion

    #region Animators
    [Header("Animators")]
    [Tooltip("Left hand animator")]
    [SerializeField]
    private Animator _leftHandAnimator;
    [Tooltip("Right hand animator")]
    [SerializeField]
    private Animator _rightHandAnimator;
    #endregion

    private void Start()
    {
        _leftGripButton.action.started += LeftGripWasPressed;
        _leftGripButton.action.canceled += LeftGriWasReleased;

        _leftTriggerButton.action.started += LeftTriggerWasPressed;
        _leftTriggerButton.action.canceled += LeftTriggerWasReleased;

        _rightGripButton.action.started += RightGripWasPressed;
        _rightGripButton.action.canceled += RightGripWasReleased;

        _rightTriggerButton.action.started += RightTriggerWasPressed;
        _rightTriggerButton.action.canceled += RightTriggerWasReleased;
    }

    #region Right Trigger
    private void RightTriggerWasPressed(InputAction.CallbackContext context)
    {
        _rightHandAnimator.SetBool("IsPointing", true);
    }

    private void RightTriggerWasReleased(InputAction.CallbackContext context)
    {
        _rightHandAnimator.SetBool("IsPointing", false);
    }
    #endregion

    #region Right Grip
    private void RightGripWasPressed(InputAction.CallbackContext context)
    {
        _rightHandAnimator.SetBool("IsFist", true);
    }
    private void RightGripWasReleased(InputAction.CallbackContext context)
    {
        _rightHandAnimator.SetBool("IsFist", false);
    }

    #endregion

    #region Left Trigger
    private void LeftTriggerWasPressed(InputAction.CallbackContext context)
    {
        _leftHandAnimator.SetBool("IsPointing", true);
    }
    private void LeftTriggerWasReleased(InputAction.CallbackContext context)
    {
        _leftHandAnimator.SetBool("IsPointing", false);
    }


    #endregion

    #region Left Grip
    private void LeftGripWasPressed(InputAction.CallbackContext context)
    {
        _leftHandAnimator.SetBool("IsFist", true);
    }

    private void LeftGriWasReleased(InputAction.CallbackContext context)
    {
        _leftHandAnimator.SetBool("IsFist", false);
    }

    #endregion

}
