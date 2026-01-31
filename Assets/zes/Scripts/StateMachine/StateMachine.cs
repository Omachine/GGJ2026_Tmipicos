using System;
using System.Collections.Generic;
using UnityEngine;

namespace FiniteStateMachine
{
    public class StateMachine
    {
        StateNode currentNode;
        Dictionary<Type, StateNode> nodes = new();
        HashSet<ITransition> anyTransitions = new();

        public void Update()
        {
            var transition = GetTransition();
            if (transition != null)
                ChangeState(transition.To);
            
            currentNode.State.Update();
        }
        
        public void FixedUpdate()
        {
            currentNode.State?.FixedUpdate();
        }

        public void SetState(IState state)
        {
            currentNode = GetOrAddNode(state);
            currentNode.State.OnEnter();
        }

        void ChangeState(IState state)
        {
            if (state == currentNode.State) return;
            
            var previousState = currentNode.State;
            var nextState = nodes[state.GetType()].State;
            
            previousState.OnExit();
            nextState.OnEnter();
            currentNode = nodes[state.GetType()];
        }

        ITransition GetTransition()
        {
            foreach (var transition in anyTransitions)
                if (transition.Condition.Evaluate())
                    return transition;
            
            foreach (var transition in currentNode.Transitions)
                if (transition.Condition.Evaluate())
                    return transition;

            return null;
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

        public void AddAnyTransition(IState to, IPredicate condition)
        {
            anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
        }
        
        StateNode GetOrAddNode(IState state)
        {
            var node = nodes.GetValueOrDefault(state.GetType());
            
            if (node == null)
            {
                node = new StateNode(state);
                nodes.Add(state.GetType(), node);
            }

            return node;
        }

        class StateNode
        {
            public IState State { get; }
            public HashSet<ITransition> Transitions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<ITransition>();
            }
            
            public void AddTransition(IState to, IPredicate condition)
            {
                Transitions.Add(new Transition(to, condition));
            }
        }
    }
}