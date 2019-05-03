using System.ComponentModel.DataAnnotations;

namespace MGS.BoilerplateDemo.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}