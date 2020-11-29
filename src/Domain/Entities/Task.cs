using System;

namespace Domain.Entities
{
    public class StateTask : BaseEntity
    {
        public string Title { get; set; }
        
        public Guid FlowId { get; set; }
        public Flow Flow { get; set; }
        
        public Guid StateId { get; set; }
        public State State { get; set; }
    }
}