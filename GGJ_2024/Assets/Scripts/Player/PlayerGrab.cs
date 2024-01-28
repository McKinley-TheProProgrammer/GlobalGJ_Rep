using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrab : MonoBehaviour
{
    private bool hold;

    [SerializeField]
    private InputAction mouseAction;
    
    void OnEnable()
    {
        mouseAction.Enable();
    }

    private void OnDisable()
    {
        mouseAction.Disable();
    }

    private void Update()
    {
        if (mouseAction.IsPressed())
        {
            hold = true;
        }
        else
        {
            hold = false;
            Destroy(GetComponent<FixedJoint2D>());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.collider.CompareTag("Grabbable") && hold)
        {
            Rigidbody2D otherRb = collision2D.transform.GetComponent<Rigidbody2D>();
            if (otherRb != null)
            {
                FixedJoint2D fj = transform.gameObject.AddComponent<FixedJoint2D>();
                fj.connectedBody = otherRb;
            }
            else
            {
                FixedJoint2D fj = transform.gameObject.AddComponent<FixedJoint2D>();
            }
        }
    }
}
