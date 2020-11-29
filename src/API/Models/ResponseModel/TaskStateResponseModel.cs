namespace API.Models.ResponseModel
{
    public class TaskStateResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public TaskResponseModel Data { get; set; }
    }
}