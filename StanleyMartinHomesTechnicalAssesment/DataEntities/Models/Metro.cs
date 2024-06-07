using System.ComponentModel.DataAnnotations;

namespace StanleyMartinHomesTechnicalAssesment.DataEntities.Models
{

    public class Metro
    {
        [Key]
        public int Id { get; set; }
        public int MetroAreaID { get; set; }
        public string MetroAreaTitle { get; set; }
        public string MetroAreaStateAbr {  get; set; }
        public string MetroAreaStateName { get; set; }
    }
}
