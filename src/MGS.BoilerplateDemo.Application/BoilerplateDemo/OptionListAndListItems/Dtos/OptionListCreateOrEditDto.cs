using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using MGS.BoilerplateDemo.BoilerplateDemo.OptionListsAndListItems;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MGS.BoilerplateDemo.BoilerplateDemo.OptionListAndListItems.Dtos
{
    [AutoMap(typeof(OptionList))]
    public class OptionListCreateOrEditDto : EntityDto, IMayHaveTenant, IPassivable
    {
        public int? TenantId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        [MaxLength(OptionList.MaxKeyLength)]
        public string OptionListKey { get; set; }

        [Required]
        [MaxLength(OptionList.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        [MaxLength(OptionList.MaxDescriptionLength)]
        public string Description { get; set; }

        public List<OptionListItemViewDto> OptionListItems { get; set; }
    }
}
