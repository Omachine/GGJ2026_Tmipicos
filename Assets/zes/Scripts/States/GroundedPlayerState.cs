using UnityEngine;

namespace FiniteStateMachine
{
    public class GroundedPlayerState : PlayerState
    {
        public GroundedPlayerState(PlayerController player, Animator animator) : base(player, animator) { }
        
        public override void OnEnter()
        {
            animator.CrossFade(GroundedHash, crossFadeDuration);
            PlayerController.isMaskOn = false;
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