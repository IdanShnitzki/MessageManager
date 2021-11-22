using System.ComponentModel.DataAnnotations;

namespace MessageManagerService.Dtos
{
    public class MessageUpdateDto
    {
        [Required]
        public string MessageStr { get; set; }
    }
}
