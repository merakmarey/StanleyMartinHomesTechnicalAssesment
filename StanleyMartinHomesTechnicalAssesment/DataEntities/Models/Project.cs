using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace StanleyMartinHomesTechnicalAssesment.DataEntities.Models
{
    
    public class Project
    {
        [Key]
        public int ProjectGroupID { get; set; }
        public int MetroAreaID { get; set; }
        public string FullName { get; set; }
        public char Status { get; set; }

    }
}
