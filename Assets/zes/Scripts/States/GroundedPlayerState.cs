using UnityEngine;

namespace FiniteStateMachine
{
    public class GroundedPlayerState : PlayerState
    {
        public GroundedPlayerState(PlayerController player) : base(player) { }
        
        public override void OnEnter()
        {
            Debug.Log("Entering Moving State");
        }

        public override void FixedUpdate()
        {
            player.HandleMovement();
        }
    }
}