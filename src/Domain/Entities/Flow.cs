using System.Collections.Generic;

namespace Domain.Entities
{
    public class Flow : BaseEntity
    {
        public string Title { get; set; }
        
        public ICollection<State> States { get; set; }
        public ICollection<StateTask> Tasks { get; set; }
    }
}