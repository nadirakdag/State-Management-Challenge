using System;

namespace API.Models.ResponseModel
{
    public class TaskResponseModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        
        public Guid StateId { get; set; }
        public string StateTitle { get; set; }
        
        public Guid FlowId { get; set; }
        public string FlowTitle { get; set; }
    }
}