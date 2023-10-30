using System.ComponentModel.DataAnnotations;

namespace Kafka.Dotnet.Storage.Entities
{
    public class Note
    {

        public Guid Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Content { get; set; }

        public Note(string content)
        {
            Id = Guid.NewGuid();
            Content = content;
        }

    }
}
