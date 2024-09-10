using Assets.Scripts.BuildingSystem;

namespace Assets.Scripts.FiniteStateMachineComponents
{
    internal class FSMPickUp : FSMState
    {
        private FiniteStateMachine _fsm;
        private ObjectPicker _picker;

        public FSMPickUp(FiniteStateMachine fsm, ObjectPicker picker)
        {
            _fsm = fsm;
            _picker = picker;           
        }

        public override void Enter()
        {
            _picker.ObjectPicked += EndState;
        }
             
        public override void Exit()
        {
            _picker.ObjectPicked -= EndState;
        }

        private void EndState()
        {
            _fsm.SetState<FSMBuild>();
            Exit();
        }
    }
}