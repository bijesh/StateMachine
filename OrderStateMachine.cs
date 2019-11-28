using System;
using System.Collections.Generic;
using Stateless;
using Stateless.Graph;

namespace StateMachine
{
    public class OrderStateMachine : IOrderStateMachine
    {
        private StateMachine<TestState, OrderEvent> _stateMachine;
        private TestState _state;
        public OrderStateMachine(TestState state)
        {
            _state = state;
            _stateMachine = new StateMachine<TestState, OrderEvent>(state);
           
            _stateMachine.Configure(TestState.Idle)
                .Permit(OrderEvent.Fetch, TestState.Loading);

            _stateMachine.Configure(TestState.Loading)
                .Permit(OrderEvent.Resolve, TestState.Success);

            _stateMachine.Configure(TestState.Loading)
                .Permit(OrderEvent.Reject, TestState.Failure);

            _stateMachine.Configure(TestState.Failure)
                .Permit(OrderEvent.Retry, TestState.Loading);


        }

        public TestState NextState(OrderEvent triggeredState)
        {
            try
            {
                _stateMachine.Fire(triggeredState);
                _state = _stateMachine.State;
                return _state;
            }
            catch (InvalidOperationException ex)
            {
                return _state;
            }
        }

        public TestState GetCurrentState()
        {
            return _state;
        }
        public IEnumerable<OrderEvent> GetAllowedTriggers()
        {
            return _stateMachine.PermittedTriggers;
        }

        public string ExportToGraph()
        {
            return UmlDotGraph.Format(_stateMachine.GetInfo());
        }
    }
}
