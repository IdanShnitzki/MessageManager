using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MessageManagerService.Controllers.Models
{
    public class Message
    {
        [Key]
        [Required]  
        public int Id { get; set; }

        [Required(ErrorMessage = "Message text is required")]
        public string MessageStr { get; set; }

        [Required]
        public bool IsPalindrome
        {
            get => IsMsgPalindrome(MessageStr);
            set => IsMsgPalindrome(MessageStr);
        }

        #region Methods
        private static bool IsMsgPalindrome(string inputStr)
        {
            if (string.IsNullOrWhiteSpace(inputStr)) return false;

            inputStr = inputStr.ToLower().Trim().Replace(" ", "");
            return inputStr.SequenceEqual(inputStr.Reverse());
        }
        #endregion
    }
}
