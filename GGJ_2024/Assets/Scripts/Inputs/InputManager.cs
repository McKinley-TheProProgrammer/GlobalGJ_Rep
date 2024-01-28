using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : SingletonGlobal<InputManager>
{
    public PlayerInput playerInput;
    
    public PlayerControls PlayerControls { get; private set; }
    
    private PlayerControls.MovementActions movementActions;
    public InputAction MoveAction => movementActions.Move;
    public InputAction JumpAction => movementActions.Jump;
    
    protected override void Awake()
    {
        base.Awake();
        PlayerControls = new PlayerControls();

        movementActions = PlayerControls.Movement;
    }

    private void OnEnable()
    {
        PlayerControls.Enable();
    }

    private void OnDisable()
    {
        PlayerControls.Disable();
    }

   

}
