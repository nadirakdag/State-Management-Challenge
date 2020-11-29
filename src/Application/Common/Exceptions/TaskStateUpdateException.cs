using System;
using Domain.Entities;

namespace Application.Common.Exceptions
{
    public class TaskStateUpdateException : Exception
    {
        public StateTask Task { get; set; }
        public TaskStateUpdateException(string message, StateTask task) : base(message)
        {
            Task = task;
        }
    }
}