namespace Core
{
    public class EventMessage
    {
        public string MessageType { get; private set; }
        public Guid AggregateId { get; private set; }

        public EventMessage(Guid aggregateId)
        {
            MessageType = GetType().Name;
            AggregateId = aggregateId;
        }
    }
}