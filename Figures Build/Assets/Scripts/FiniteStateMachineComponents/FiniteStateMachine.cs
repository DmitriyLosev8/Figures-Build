using System;
using System.Collections.Generic;

namespace Assets.Scripts.FiniteStateMachineComponents
{
    internal class FiniteStateMachine
    {
        private Dictionary<Type, FSMState> _states = new Dictionary<Type, FSMState>();

        public bool IsEnemy { get; private set; }

        public FSMState CurrentState { get; private set; }

        public void AddState(FSMState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void SetTypeOfCharacter(bool isEnemy)
        {
            IsEnemy = isEnemy;
        }

        public void SetState<T>() where T : FSMState
        {
            var type = typeof(T);

            if (CurrentState != null && CurrentState.GetType() == type)
            {
                return;
            }

            if (_states.TryGetValue(type, out var newState))
            {
                CurrentState?.Exit();
                CurrentState = newState;
                CurrentState.Enter();
            }
        }

        public void Update()
        {
            CurrentState?.Update();
        }
    }
}
