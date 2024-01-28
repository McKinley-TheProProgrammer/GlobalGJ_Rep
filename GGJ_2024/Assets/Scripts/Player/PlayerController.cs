using System;
using Unity.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;
    private Animator _animator;

    private InputManager _inputManager;
    
    [SerializeField]
    private float moveSpeed = 30;
    [SerializeField] 
    private float jumpForce = 400f;
    
    [SerializeField] 
    private Transform[] feetPos;
    public float feetRadius = .6f;
    public LayerMask groundLayer;

    [ReadOnly]
    public bool isGrounded;

    void Start()
    {
        _animator = GetComponent<Animator>();
        
        _inputManager = InputManager.Instance;

        Collider2D[] colliders = transform.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            for (int k = i + 1; k < colliders.Length; k++)
            {
                Physics2D.IgnoreCollision(colliders[i],colliders[k]);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xAxis = _inputManager.MoveAction.ReadValue<float>();

        if (xAxis != 0)
        {
            if (xAxis > 0)
            {
                _animator.Play("Walk");
                _rb.AddForce(Vector2.right * moveSpeed);
            }
            else
            {
                _animator.Play("WalkBack");
                _rb.AddForce(Vector2.left * moveSpeed);
            }
        }
        else
        {
            _animator.Play("Idle");
        }

        foreach (var feet in feetPos)
        {
            isGrounded = Physics2D.OverlapCircle(feet.position, feetRadius, groundLayer);
        }

        if (isGrounded && _inputManager.JumpAction.triggered)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        if(feetPos.Length == 0)
            return;
        
        Gizmos.color = Color.magenta;
        foreach (var feet in feetPos)
        {
            Gizmos.DrawWireSphere(feet.position,feetRadius);
        }
        
    }
}
