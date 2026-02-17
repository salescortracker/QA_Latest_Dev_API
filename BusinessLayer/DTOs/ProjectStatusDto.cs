namespace BusinessLayer.DTOs
{
    public class ProjectStatusDto
    {
        public int ProjectStatusId { get; set; }
        public int CompanyId { get; set; }
        public int RegionId { get; set; }
        public string ProjectStatusName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int? UserId { get; set; }
    
    }
}
