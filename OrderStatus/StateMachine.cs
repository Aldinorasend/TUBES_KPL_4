using OrderLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine
{
   /* public enum OrderState
    {
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
        public Dictionary<(OrderStatus, OrderEvent), OrderStatus> _transitions;
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
        }

        public bool ApplyEvent(OrderEvent orderEvent)
        {
            var key = (CurrentState, orderEvent);
            if (_transitions.TryGetValue(key, out var newState))
            {
                CurrentState = newState;
                return true;
            }
            return false;
        }
    }
    public class OrderIn
    {
        public int Id { get; set; }
        public List<MenuItem> Menu { get; set; }
        public string Description { get; set; }
        public OrderStatus Status => StateMachine.CurrentState;

        public OrderStateMachine StateMachine { get; set; }

        public OrderIn(int idOrder, List<MenuItem> menu, string description)
        {
            Id = idOrder;
            Menu = menu;
            Description = description;
            StateMachine = new OrderStateMachine();
        }

        public bool ApplyEvent(OrderEvent orderEvent)
        {
            return StateMachine.ApplyEvent(orderEvent);
        }
    }
*/

}
