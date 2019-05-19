using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGS.BoilerplateDemo.BoilerplateDemo.Contacts.Dtos
{
    [AutoMap(typeof(Contact))]
    public class ContactCreateOrEditDto : EntityDto, IMustHaveTenant, IPassivable
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required]
        public int StateId { get; set; }

        public int TenantId { get; set; }
        public bool IsActive { get; set; }
        public int? TitleId { get; set; }
    }
}
