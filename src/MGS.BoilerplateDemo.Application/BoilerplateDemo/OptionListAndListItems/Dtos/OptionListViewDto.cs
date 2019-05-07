using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MGS.BoilerplateDemo.BoilerplateDemo.OptionListsAndListItems;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MGS.BoilerplateDemo.BoilerplateDemo.OptionListAndListItems.Dtos
{
    [AutoMap(typeof(OptionList))]
    public class OptionListViewDto : FullAuditedEntityDto
    {
        [Required]
        [MaxLength(OptionList.MaxKeyLength)]
        public string OptionListKey { get; set; }

        [MaxLength(OptionList.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        [MaxLength(OptionList.MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public int? TenantId { get; set; }

        public IList<OptionListItemViewDto> OptionListItems { get; set; } = new List<OptionListItemViewDto>();
    }
}
