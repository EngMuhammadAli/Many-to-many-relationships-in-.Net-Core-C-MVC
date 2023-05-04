using System.ComponentModel.DataAnnotations;

namespace MvcMany.Models.AccountModel
{
    public class Role
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
       
    }
}
