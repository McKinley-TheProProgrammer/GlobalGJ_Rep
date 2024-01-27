using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : SingletonGlobal<InputManager>
{
    public PlayerInput playerInput;
    
    public PlayerControls PlayerControls { get; private set; }
    
    public PlayerControls.MovementActions movementActions;

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
