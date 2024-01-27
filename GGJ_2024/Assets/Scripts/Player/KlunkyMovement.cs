using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class KlunkyMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float moveSpeed = 30;
    
    [SerializeField] 
    private Transform feetPos;
    
    [ReadOnly]
    public bool isGrounded;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
