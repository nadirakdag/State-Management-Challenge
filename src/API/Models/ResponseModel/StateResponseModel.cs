using System;

namespace API.Models.ResponseModel
{
    public class StateResponseModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        
        public Guid FlowId { get; set; }
        public string FlowTitle { get; set; }
        
        public Guid? NextStateId { get; set; }
        public string NextStateTitle { get; set; }
        
        public Guid? PrevStateId { get; set; }
        public string PrevStateTitle { get; set; }
    }
}