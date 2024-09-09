using UnityEngine;
using Assets.Scripts.FiniteStateMachineComponents;
using Assets.Scripts.BuildingSystem;

namespace Assets.Scripts.PlayerComponents
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private ObjectPicker _picker;
        [SerializeField] private ObjectMover _objectMover;

        private FiniteStateMachine _fsm;

        private void Start()
        {
            InitFsm();
        }

        private void Update()
        {
            _fsm.Update();
        }

        private void InitFsm()
        {
            _fsm = new FiniteStateMachine();
            _fsm.AddState(new FSMPickUp(_fsm, _picker));
            _fsm.AddState(new FSMBuild(_fsm, _picker, _objectMover));
            _fsm.SetState<FSMPickUp>();
        }
    }
}