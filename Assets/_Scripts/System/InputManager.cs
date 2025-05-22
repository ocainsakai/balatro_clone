using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private InputSystem_Actions action;
    public Action OnDraw;
    public Action OnDiscard;
    public Action OnPlay;
    public Action OnReset;
    private void Awake()
    {
        action = new InputSystem_Actions();
        action.Action.Play.performed += _ => OnPlay?.Invoke();
        action.Action.Draw.performed += _ => OnDraw?.Invoke();
        action.Action.Discard.performed += _ => OnDiscard?.Invoke();
        action.Action.Reset.performed += _ => OnReset?.Invoke();
    }
    #region ON-OFF
    private void OnEnable()
    {
        EnableInput();
    }
    private void OnDisable()
    {
        DisableInput();
    }
    public void DisableInput()
    {
        action.Disable();
    }
    public void EnableInput()
    {
        action.Enable();
    }
    #endregion
}
