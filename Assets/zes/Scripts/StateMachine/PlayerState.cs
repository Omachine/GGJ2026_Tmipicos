using UnityEngine;

namespace FiniteStateMachine
{
    public abstract class PlayerState : IState {
        protected readonly PlayerController player;
        
        protected PlayerState(PlayerController player)
        {
            this.player = player;
        }
        
        public virtual void OnEnter() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void OnExit() { }
    }
}