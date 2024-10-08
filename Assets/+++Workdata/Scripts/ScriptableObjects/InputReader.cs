using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Scriptable Objects/InputReader")]
public class InputReader : ScriptableObject
{
    public InputAction moveAction;
    public InputAction runAction;
    public InputAction interactAction;
    public InputAction attackAction;
    public InputAction inventoryAction;
    public InputAction toolAction;
    public InputAction pauseAction;
    
    private Player_InputActions _inputActions;
   
    private void OnEnable()
    {
        _inputActions = new Player_InputActions();
        moveAction = _inputActions.Player.Move;
        runAction = _inputActions.Player.Run;
        interactAction = _inputActions.Player.Interact;
        attackAction = _inputActions.Player.Attack;
        toolAction = _inputActions.Player.Tool;
        inventoryAction = _inputActions.UI.Inventory;
        pauseAction = _inputActions.UI.Pause;
    }

    public void Enable()
    {
        _inputActions.Enable();
    }

    public void Disable()
    {
        _inputActions.Disable();
    }

    public void EnablePlayerInput()
    {
        _inputActions.Player.Enable();
    }

    public void DisablePlayerInput()
    {
        _inputActions.Player.Disable();
    }

    public bool GetPlayerEnabled()
    {
        return _inputActions.Player.enabled;
    }
}
