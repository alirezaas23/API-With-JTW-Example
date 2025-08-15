using System.ComponentModel.DataAnnotations;

namespace JWTTraining.Dtos.Blog
{
    public class UpdateBlogDto
    {
        [Required, MaxLength(200)]
        public string Title { get; set; }
        [Required, MaxLength(100)]
        public string Author { get; set; }
        [Required, MaxLength(500)]
        public string Description { get; set; }
    }
}
