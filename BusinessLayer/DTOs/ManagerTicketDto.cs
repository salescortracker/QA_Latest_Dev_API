namespace BusinessLayer.DTOs
{
    public class ManagerTicketDto
    {
        public int TicketId { get; set; }
        public string TicketNumber { get; set; } = null!;
        public string EmployeeName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string PriorityName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string? ManagerComments { get; set; }
    }
}
