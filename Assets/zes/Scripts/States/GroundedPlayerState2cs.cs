using UnityEngine;

namespace FiniteStateMachine
{
    public class GroundedPlayerState2 : PlayerState
    {
        public GroundedPlayerState2(PlayerController player, Animator animator) : base(player, animator) { }
        
        public override void OnEnter()
        {
            player.Mask2.SetActive(true);
            player.Mask1.SetActive(false);
            animator.CrossFade(SpectralHash, crossFadeDuration);
            PlayerController.isMaskOn = true;
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