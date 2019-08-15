using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MGS.BoilerplateDemo.BoilerplateDemo.OptionListsAndListItems;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MGS.BoilerplateDemo.BoilerplateDemo.Contacts
{
    public class Contact : FullAuditedEntity, IPassivable, IMustHaveTenant
    {
        //an optional local list item
        [ForeignKey("TitleId")]
        public virtual OptionListItem Title { get; set; }
        public int? TitleId { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        //make the State Required
        [ForeignKey("StateId")]
        public virtual OptionListItem State { get; set; }
        [Required]
        public int StateId { get; set; }

        /* TODO: Other contact fields not relevant to demo */

        [Required]
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
    }
}
