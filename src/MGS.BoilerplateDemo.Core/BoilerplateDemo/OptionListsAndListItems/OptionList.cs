using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MGS.BoilerplateDemo.BoilerplateDemo.OptionListsAndListItems
{
    public class OptionList : FullAuditedEntity, IPassivable, IMayHaveTenant
    {
        public const int MaxKeyLength = 128;
        public const int MaxDescriptionLength = 256;
        public const int MaxDisplayNameLength = 32;

        [Required]
        [MaxLength(MaxKeyLength)]
        public string OptionListKey { get; set; }

        [Required]
        [MaxLength(MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool IsActive { get; set; }

        public int? TenantId { get; set; }

        public virtual IList<OptionListItem> OptionListItems { get; set; } = new List<OptionListItem>();

        public void Update(OptionList input)
        {
            var updateTime = DateTime.Now;
            OptionListKey = input.OptionListKey;
            Description = input.Description;
            DisplayName = input.DisplayName;
            IsActive = input.IsActive;
            IsDeleted = input.IsDeleted;
            LastModificationTime = updateTime;
        }
    }
}
