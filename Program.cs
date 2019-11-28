using System;

namespace StateMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var orderStateMachine = new OrderStateMachine(TestState.Idle);
            var nextState = orderStateMachine.NextState(OrderEvent.Fetch);
            Console.WriteLine($"Next State is {nextState}");
            Console.ReadLine();

        }
    }
}
