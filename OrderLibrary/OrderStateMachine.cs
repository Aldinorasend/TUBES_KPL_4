using OrderLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace OrderLibrary
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Completed,
        Cancelled
    }

    public enum OrderEvent
    {
        FinishProcessing,
        CancelOrder
    }

    public class OrderStateMachine
    {
        private Dictionary<(OrderStatus, OrderEvent), OrderStatus> _transitions;
        public OrderStatus CurrentState { get; private set; }

        public OrderStateMachine()
        {
            CurrentState = OrderStatus.Pending;

            _transitions = new Dictionary<(OrderStatus, OrderEvent), OrderStatus>
            {
                { (OrderStatus.Pending, OrderEvent.FinishProcessing), OrderStatus.Processing },
                { (OrderStatus.Processing, OrderEvent.FinishProcessing), OrderStatus.Completed },
                { (OrderStatus.Pending, OrderEvent.CancelOrder), OrderStatus.Cancelled },
                { (OrderStatus.Processing, OrderEvent.CancelOrder), OrderStatus.Cancelled }
                // Completed and Cancelled states are terminal; no transitions from here
            };

            // Class Invariant: CurrentState should always be a valid OrderStatus
            Debug.Assert(Enum.IsDefined(typeof(OrderStatus), CurrentState), "Invariant: CurrentState must be a valid OrderStatus");
        }

        public bool ApplyEvent(OrderEvent orderEvent)
        {
            // Precondition: CurrentState must be valid
            Debug.Assert(Enum.IsDefined(typeof(OrderStatus), CurrentState), "Precondition: CurrentState must be a valid OrderStatus");

            var key = (CurrentState, orderEvent);

            // Precondition: The event must lead to a valid state transition
            if (!_transitions.ContainsKey(key))
            {
                Debug.Fail($"Precondition failed: Invalid state transition from {CurrentState} using event {orderEvent}");
                return false;
            }

            var newState = _transitions[key];

            // Postcondition: newState must be a valid OrderStatus
            Debug.Assert(Enum.IsDefined(typeof(OrderStatus), newState), "Postcondition: newState must be a valid OrderStatus");

            CurrentState = newState;

            // Invariant: After state transition, CurrentState must be valid
            Debug.Assert(Enum.IsDefined(typeof(OrderStatus), CurrentState), "Invariant: CurrentState must be a valid OrderStatus after state transition");

            return true;
        }
    }
}
