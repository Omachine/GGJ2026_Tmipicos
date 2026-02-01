using System;
using UnityEngine;
using FiniteStateMachine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public static bool isInDoll = false;
    public Rigidbody rb;
    public Animator animator;
    
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float acceleration;
    
    StateMachine stateMachine;

    public Transform FPCamera;

    public GameObject DollCamera;
    public GameObject UI;
    public GameObject Mask1, Mask2;
    
    Vector3 movementInput;
    Vector3 wishedDirection; // movement direction with speed and camera perspective applied

    private void Awake()
    {
        animator = GetComponent<Animator>();
        // State Machine Initialization
        stateMachine = new StateMachine();
        
        var idleState = new GroundedPlayerState(this, animator);
        var spectralState = new GroundedPlayerState1(this, animator);
        var spectralState2 = new GroundedPlayerState2(this, animator);
        var dollState = new DollPlayerState(this, animator);
        
        stateMachine.AddTransition(idleState, spectralState, new FuncPredicate(() => Input.GetKeyDown(KeyCode.Space)));
        stateMachine.AddTransition(idleState, spectralState2, new FuncPredicate(() => Input.GetKeyDown(KeyCode.Space)));
        stateMachine.AddTransition(spectralState, idleState, new FuncPredicate(() => Input.GetKeyDown(KeyCode.Space)));
        stateMachine.AddTransition(spectralState2, idleState, new FuncPredicate(() => Input.GetKeyDown(KeyCode.Space)));
        
        stateMachine.AddTransition(idleState, dollState, new FuncPredicate(() => isInDoll));
        stateMachine.AddTransition(dollState, idleState, new FuncPredicate(() => Input.GetKeyDown(KeyCode.Escape)));
        
        stateMachine.SetState(idleState);
        
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