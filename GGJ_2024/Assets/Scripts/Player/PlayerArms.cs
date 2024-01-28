using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerArms : MonoBehaviour
{
    private int speed = 300;

    public Rigidbody2D rb;

    private InputManager inputManager;
    
    [SerializeField]
    private InputAction mouseAction;

    private void OnEnable()
    {
        mouseAction.Enable();
    }

    private void OnDisable()
    {
        mouseAction.Disable();
    }

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    private void Update()
    {
        Vector2 mouseAim = (inputManager.MousePosition - transform.position);
        float rotationZ = Mathf.Atan2(mouseAim.y, mouseAim.x) * Mathf.Rad2Deg;
        
        if (mouseAction.IsPressed())
        {
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation,rotationZ,speed * Time.fixedDeltaTime));
        }
    }

    private void OnDrawGizmos()
    {
        if(!Application.isPlaying)
            return;
        
        Vector2 mouseAim = (inputManager.MousePosition - transform.position);
        
        Gizmos.DrawLine(rb.position,mouseAim);
    }
}
