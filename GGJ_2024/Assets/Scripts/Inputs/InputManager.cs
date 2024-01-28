using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : SingletonGlobal<InputManager>
{
    [SerializeField] 
    public CinemachineBrain mainBrain;
    
    public PlayerInput playerInput;
    public PlayerControls PlayerControls { get; private set; }
    
    private PlayerControls.MovementActions movementActions;
    public InputAction MoveAction => movementActions.Move;
    public InputAction JumpAction => movementActions.Jump;
    public InputAction MouseLeftBtnAction => movementActions.MouseLeftPress;
    public Vector3 MousePosition => CamUtils.ScreenToWorldPosition(mainBrain,movementActions.CursorPosition.ReadValue<Vector2>());

    public bool Pause { get; set; }

    public void PPause() => Pause = true;
    public void Unpause() => Pause = false;

    protected override void Awake()
    {
        base.Awake();
        PlayerControls = new PlayerControls();
        
        movementActions = PlayerControls.Movement;

        mainBrain = GameObject.FindWithTag("MainCamera").GetComponent<CinemachineBrain>();
    }

    private void OnEnable()
    {
        PlayerControls.Enable();
    }

    private void OnDisable()
    {
        PlayerControls.Disable();
    }

    private void Update()
    {
        if (Pause)
        {
            PlayerControls.Disable();
        }
        else if (!Pause)
        {
            PlayerControls.Enable();
        }
    }
}
