using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWTTraining.Entities
{
    public class Blog
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(100)]
        public string Author { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }

        public Blog(string title, string author, string description)
        {
            Title = title;
            Author = author;
            Description = description;
        }
    }
}
