using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace Domain.Entities
{
    public class State : BaseEntity
    {
        public string Title { get; set; }
        
        public Guid FlowId { get; set; }
        public Flow Flow { get; set; }
        
        public Guid? PrevStateId { get; set; }
        public virtual State PrevState { get; set; }
        
        public Guid? NextStateId { get; set; }
        public virtual State NextState { get; set; }
        
        
        public ICollection<StateTask> Task { get; set; }
    }
}