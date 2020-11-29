using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class State : BaseEntity
    {
        public string Title { get; set; }
        
        public Guid FlowId { get; set; }
        public Flow Flow { get; set; }
        
        public Guid? PrevStateId { get; set; }
        public Guid? NextStateId { get; set; }
        
        public ICollection<StateTask> Task { get; set; }
    }
}