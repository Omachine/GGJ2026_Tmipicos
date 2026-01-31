using System;
using UnityEngine;
using FiniteStateMachine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float acceleration;
    
    StateMachine stateMachine;

    public Transform FPCamera;
    
    Vector3 movementInput;
    Vector3 wishedDirection; // movement direction with speed and camera perspective applied

    private void Awake()
    {
        // State Machine Initialization
        stateMachine = new StateMachine();
        
        var movingState = new GroundedPlayerState(this);
        
        //stateMachine.AddTransition(movingState, jumpState, new FuncPredicate(() => jumpedLastFrame));
        
        stateMachine.SetState(movingState);
        
        rb.freezeRotation = true;
    }

    private void Update()
    {
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        wishedDirection = Quaternion.AngleAxis(FPCamera.eulerAngles.y, Vector3.up) * movementInput;
        
        // Rotate the player based on camera direction
        transform.rotation = Quaternion.AngleAxis(FPCamera.eulerAngles.y, Vector3.up);
        stateMachine.Update();
    }

    private void FixedUpdate() => stateMachine.FixedUpdate();

    public void HandleMovement()
    {
        Vector3 targetVelocity = wishedDirection * speed;
        // Remove a velocidade anterior visto que está a ser adicionada e não substituída
        Vector3 horizontalVelocity = new (rb.linearVelocity.x, 0, rb.linearVelocity.z);
        Vector3 velocityDifference = targetVelocity - horizontalVelocity;
        
        Vector3 movementForce = velocityDifference * acceleration;
        
        rb.AddForce(movementForce);
    }
}