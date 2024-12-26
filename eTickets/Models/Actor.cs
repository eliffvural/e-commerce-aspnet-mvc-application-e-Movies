using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Actor : IEntityBase
    {
        [Key]
        public int Id { get; set; }


        [Display(Name = "Profile picture")]
        [Required(ErrorMessage =" Profile Picture is required. ")]
        public String ProfilePictureURL { get; set; }


        [Display(Name = "Full Name")]
        [Required(ErrorMessage = " Full Name is required. ")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 2 and 50 characters. ")]

        public string FullName { get; set; }


        [Display(Name = "Biography")]
        [Required(ErrorMessage = " Biography is required. ")]

        public string Bio { get; set; }

        //Relationships

        public List<Actor_Movie>Actor_Movies { get; set; }


    }
}
