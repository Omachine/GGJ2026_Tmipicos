using UnityEngine;

namespace FiniteStateMachine
{
    public class GroundedPlayerState1 : PlayerState
    {
        public GroundedPlayerState1(PlayerController player, Animator animator) : base(player, animator) { }
        
        public override void OnEnter()
        {
            player.Mask2.SetActive(false);
            player.Mask1.SetActive(true);
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