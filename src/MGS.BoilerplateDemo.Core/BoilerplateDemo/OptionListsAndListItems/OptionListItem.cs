using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace MGS.BoilerplateDemo.BoilerplateDemo.OptionListsAndListItems
{
    public class OptionListItem : FullAuditedEntity, IPassivable
    {
        public const int MaxItemDisplayTextLength = 32;
        public const int MaxAdditionalInfoLength = 128;
        public const int MinDisplayOrderValue = 1;
        public const int MaxDisplayOrderValue = int.MaxValue;

        public virtual OptionList OptionList { get; set; } = new OptionList();
        public int OptionListId { get; set; }

        [Required]
        [MaxLength(MaxItemDisplayTextLength)]
        public string DisplayText { get; set; }

        [MaxLength(MaxAdditionalInfoLength)]
        public string AdditionalInfo { get; set; }

        [Range(MinDisplayOrderValue, MaxDisplayOrderValue)]
        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public void Update(OptionListItem item)
        {
            IsActive = item.IsActive;
            DisplayText = item.DisplayText;
            AdditionalInfo = item.AdditionalInfo;
            DisplayOrder = item.DisplayOrder;
        }
    }
}