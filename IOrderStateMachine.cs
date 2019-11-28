using System.Collections.Generic;

namespace StateMachine
{
    public interface IOrderStateMachine
    {
        TestState NextState(OrderEvent triggeredState);
        IEnumerable<OrderEvent> GetAllowedTriggers();
        string ExportToGraph();
        TestState GetCurrentState();
    }
}