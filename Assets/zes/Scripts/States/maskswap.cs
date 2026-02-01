using UnityEngine;

namespace FiniteStateMachine
{
    public class MaskSwapState : PlayerState
    {
        public MaskSwapState(PlayerController player, Animator animator) : base(player, animator) { }
        
        public override void OnEnter()
        {
            animator.CrossFade(GroundedHash, crossFadeDuration);
        }

        public override void Update()
        {
            FPCameraController.instance.UpdateCamera();
        }

        public override void FixedUpdate()
        {
            player.HandleMovement();
        }
    }
}