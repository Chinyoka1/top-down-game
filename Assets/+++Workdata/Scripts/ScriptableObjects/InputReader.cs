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
    
    private Player_InputActions _inputActions;
   
    private void Awake()
    {
        _inputActions = new Player_InputActions();
        moveAction = _inputActions.Player.Move;
        runAction = _inputActions.Player.Run;
        interactAction = _inputActions.Player.Interact;
        attackAction = _inputActions.Player.Attack;
        inventoryAction = _inputActions.UI.Inventory;
    }

    public void Enable()
    {
        _inputActions.Enable();
    }

    public void Disable()
    {
        _inputActions.Disable();
    }

    public bool GetPlayerEnabled()
    {
        return _inputActions.Player.enabled;
    }
}
