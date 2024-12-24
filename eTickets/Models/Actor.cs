using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        public String ProfilePictureURL { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }


    }
}
