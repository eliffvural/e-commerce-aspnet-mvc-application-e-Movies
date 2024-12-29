using eTickets.Data;
using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class NewMovieVM
    {
        [Display(Description="Movie name")]
        [Required(ErrorMessage ="Name is required.")]
        public string Name { get; set; }


        [Display(Description = "Movie description")]
        [Required(ErrorMessage = "Description is required.")]
        public String Description { get; set; }


        [Display(Description = "Price in $")]
        [Required(ErrorMessage = "Price is required.")]
        public double Price { get; set; }



        [Display(Description = "Movie Poster URL")]
        [Required(ErrorMessage = "Movie poster URL is required.")]
        public string ImageURL { get; set; }

        [Display(Description = "Movie start date")]
        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }


        [Display(Description = "Movie end date")]
        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }


        [Display(Description = "Select a category")]
        [Required(ErrorMessage = "Movie Category is required.")]
        public MovieCategory MovieCategory { get; set; }


        //Relationships

        [Display(Description = "Select an actor(s)")]
        [Required(ErrorMessage = "Movie Actor(s) is required.")]
        public List<int> ActorIds { get; set; }

        //Cinema
        [Display(Description = "Select a cinema")]
        [Required(ErrorMessage = "Movie name is required.")]
        public int CinemaId { get; set; }

        //Producer
        [Display(Description = "Select a producer")]
        [Required(ErrorMessage = "Movie producer is required.")]
        public int ProducerId { get; set; }
        

    }
}
