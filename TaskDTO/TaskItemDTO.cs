namespace TaskAppDTO
{
    public class TaskItemDTO
    {
        public int TaskId { get; set; }
        public string? Title { get; set; }
        public string? Summary { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CompletedDate { get; set; }
        public int UserId { get; set; }
    }
}
