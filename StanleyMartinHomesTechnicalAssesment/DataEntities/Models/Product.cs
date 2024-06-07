using System.ComponentModel.DataAnnotations;

namespace StanleyMartinHomesTechnicalAssesment.DataEntities.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string? ProductID  { get; set; }
        public string? ProductName { get; set; }
        public string ProjectName { get; set; }
        public int ProjectGroupID { get; set; }
    }
}
