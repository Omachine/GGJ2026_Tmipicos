using UnityEngine;

namespace FiniteStateMachine
{
    public abstract class PlayerState : IState {
        protected readonly PlayerController player;
        protected readonly Animator animator;
        
        protected static readonly int GroundedHash = Animator.StringToHash("Idle");
        protected static readonly int SpectralHash = Animator.StringToHash("Spectral");
        
        protected const float crossFadeDuration = 0.2f;
        
        protected PlayerState(PlayerController player, Animator animator)
        {
            this.player = player;
            this.animator = animator;
        }
        
        public virtual void OnEnter() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void OnExit() { }
    }
}