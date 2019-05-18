using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using MGS.BoilerplateDemo.BoilerplateDemo.OptionListsAndListItems;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MGS.BoilerplateDemo.BoilerplateDemo.OptionListAndListItems.Dtos
{
    [AutoMap(typeof(OptionListItem))]
    public class OptionListItemViewDto : FullAuditedEntityDto, IPassivable
    {
        public int OptionListId { get; set; }

        [JsonIgnore]
        public OptionListViewDto OptionList { get; set; } = new OptionListViewDto();

        public string OptionListDisplayName => OptionList?.DisplayName;

        [Required]
        [MaxLength(OptionListItem.MaxItemDisplayTextLength)]
        public string DisplayText { get; set; }

        [MaxLength(OptionListItem.MaxAdditionalInfoLength)]
        public string AdditionalInfo { get; set; }

        [Range(OptionListItem.MinDisplayOrderValue, OptionListItem.MaxDisplayOrderValue)]
        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }
    }
}
