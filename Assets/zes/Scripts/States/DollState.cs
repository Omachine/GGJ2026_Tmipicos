using UnityEngine;

namespace FiniteStateMachine
{
    public class DollPlayerState : PlayerState
    {
        public DollPlayerState(PlayerController player, Animator animator) : base(player, animator) { }
        
        public override void OnEnter()
        {
            player.DollCamera.SetActive(true);
            player.UI.SetActive(true);
            Debug.Log("Entering Moving State");
            animator.CrossFade(GroundedHash, crossFadeDuration);
        }

        public override void OnExit()
        {
            player.DollCamera.SetActive(false);
            player.UI.SetActive(false);
        }
    }
}