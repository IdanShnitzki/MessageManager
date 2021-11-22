using System.ComponentModel.DataAnnotations;

namespace MessageManagerService.Dtos
{
    public class MessageCreateDto
    {
        [Required]
        public string MessageStr { get; set; }
    }
}
