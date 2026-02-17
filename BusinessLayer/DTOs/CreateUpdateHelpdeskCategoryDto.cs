namespace BusinessLayer.DTOs
{
    public class CreateUpdateHelpdeskCategoryDto
    {
        public int HelpdeskCategoryID { get; set; }
        public int CompanyID { get; set; }
        public int RegionID { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int UserId { get; set; }
    }
}
