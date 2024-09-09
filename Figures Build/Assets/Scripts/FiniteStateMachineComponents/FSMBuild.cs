using Assets.Scripts.BuildingSystem;

namespace Assets.Scripts.FiniteStateMachineComponents
{
    internal class FSMBuild : FSMState
    {
        private FiniteStateMachine _fsm;
        private ObjectPicker _picker;
        private ObjectMover _objectMover;

        public FSMBuild(FiniteStateMachine fsm, ObjectPicker picker, ObjectMover objectMover)
        {
            _fsm = fsm;
            _picker = picker;
            _objectMover = objectMover;
        }

        public override void Enter()
        {
            _objectMover.StartMove(_picker.CurrentPickingObject, _picker.SpotOfObject);
            _picker.gameObject.SetActive(false);
            _objectMover.ObjectSeted += EndState;
        }


        public override void Exit()
        {
            _objectMover.ObjectSeted -= EndState;
        }

        private void EndState()
        {
            _fsm.SetState<FSMPickUp>();
            _picker.gameObject.SetActive(true);
            Exit();
        }
    }
}