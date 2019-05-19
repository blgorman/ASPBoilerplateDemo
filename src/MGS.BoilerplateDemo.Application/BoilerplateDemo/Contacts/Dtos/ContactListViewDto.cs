using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MGS.BoilerplateDemo.BoilerplateDemo.OptionListAndListItems.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGS.BoilerplateDemo.BoilerplateDemo.Contacts.Dtos
{
    [AutoMap(typeof(Contact))]
    public class ContactListViewDto : EntityDto
    {
        public virtual OptionListItemViewDto Title { get; set; }
        public int? TitleId { get; set; }
        public string TitleDisplay => Title?.DisplayText; 

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        //make the State Required
        public virtual OptionListItemViewDto State { get; set; }
        [Required]
        public int StateId { get; set; }
        public string StateDisplay => State?.DisplayText;

        /* TODO: Other contact fields not relevant to demo */

        [Required]
        public int TenantId { get; set; }
        public bool IsActive { get; set; }

        public string DisplayName => $"{LastName}, {FirstName}";
    }
}
