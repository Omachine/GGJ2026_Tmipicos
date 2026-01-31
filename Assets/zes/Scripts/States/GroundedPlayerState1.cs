using UnityEngine;

namespace FiniteStateMachine
{
    public class GroundedPlayerState1 : PlayerState
    {
        public GroundedPlayerState1(PlayerController player, Animator animator) : base(player, animator) { }
        
        public override void OnEnter()
        {
            animator.CrossFade(SpectralHash, crossFadeDuration);
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