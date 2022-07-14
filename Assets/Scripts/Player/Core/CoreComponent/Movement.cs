using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D rb {get; private set;}
    
    protected override void Awake() 
    {
        base.Awake();

        rb = GetComponentInParent<Rigidbody2D>();
    }

    public void Move(Vector2 velocity)
    {
        rb.MovePosition((Vector2)rb.transform.position + velocity * Time.deltaTime);
    }

    public void SetVelocity(Vector2 velocity)
    {
        StopMoving();
        
        rb.velocity = velocity;
    }

    public void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }

    public void MultiplyVelocityFactor(float value)
    {
        rb.velocity *= value;
    }

    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }

    public float GetVelocityMagnitude()
    {
        return rb.velocity.magnitude;
    }

    public void AddForce(Vector2 force, ForceMode2D forceMode2D)
    {
        rb.AddForce(force, forceMode2D);
    }
}
