using UnityEngine;

public class Balance : MonoBehaviour
{
    public float targetRotation;
    public Rigidbody2D rb;
    public float force;

    private void Update()
    {
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation,targetRotation,force * Time.fixedDeltaTime));
    }
}
