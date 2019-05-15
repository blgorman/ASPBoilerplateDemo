using MGS.BoilerplateDemo.BoilerplateDemo.OptionListsAndListItems;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGS.BoilerplateDemo.EntityFrameworkCore.Seed.Host
{
    public class DefaultListAndListItemsCreator
    {
        private readonly BoilerplateDemoDbContext _context;

        public DefaultListAndListItemsCreator(BoilerplateDemoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            var QuestionList = _context.OptionLists.FirstOrDefault(x => x.OptionListKey.Equals("QuestionTypes", StringComparison.CurrentCultureIgnoreCase));
            if (QuestionList == null)
            {
                CreateList("States", "States", "A list of US States");
            }
        }

        private void CreateList(string key, string displayName, string description)
        {
            var existing = _context.OptionLists.ToList();
            var exists = existing.FirstOrDefault(x => x.DisplayName.Equals(displayName, StringComparison.OrdinalIgnoreCase));
            if (exists != null) return;

            var ol = new OptionList()
            {
                DisplayName = displayName,
                Description = description,
                IsActive = true,
                IsDeleted = false,
                TenantId = null,
                OptionListKey = key
            };

            _context.OptionLists.Add(ol);
            _context.SaveChanges();

            var theList = _context.OptionLists.Include(x => x.OptionListItems).FirstOrDefault(y => y.OptionListKey.Equals(key, StringComparison.CurrentCultureIgnoreCase));
            if (theList != null)
            {
                var modified = false;
                if (theList.OptionListItems.FirstOrDefault(x => x.DisplayText.Equals("Alabama", StringComparison.CurrentCultureIgnoreCase)) == null)
                {
                    CreateListItem(theList.Id, "Alabama", "AL", 1);
                    modified = true;
                }
                if (theList.OptionListItems.FirstOrDefault(x => x.DisplayText.Equals("Alaska", StringComparison.CurrentCultureIgnoreCase)) == null)
                {
                    CreateListItem(theList.Id, "Alaska", "AK", 2);
                    modified = true;
                }
                if (theList.OptionListItems.FirstOrDefault(x => x.DisplayText.Equals("Arizona", StringComparison.CurrentCultureIgnoreCase)) == null)
                {
                    CreateListItem(theList.Id, "Arizona", "AZ", 3);
                    modified = true;
                }
                if (theList.OptionListItems.FirstOrDefault(x => x.DisplayText.Equals("Arkansas", StringComparison.CurrentCultureIgnoreCase)) == null)
                {
                    CreateListItem(theList.Id, "Arkansas", "AR", 4);
                    modified = true;
                }
                if (theList.OptionListItems.FirstOrDefault(x => x.DisplayText.Equals("California", StringComparison.CurrentCultureIgnoreCase)) == null)
                {
                    CreateListItem(theList.Id, "California", "CA", 5);
                    modified = true;
                }
                // TODO: add the rest of the states..

                if (modified) _context.SaveChanges();
            }
        }

        private void CreateListItem(int listId, string displayText, string additionalInfo, int displayOrder)
        {
            var oli = new OptionListItem()
            {
                AdditionalInfo = additionalInfo,
                DisplayOrder = displayOrder,
                DisplayText = displayText,
                IsActive = true,
                IsDeleted = false,
                OptionListId = listId
            };

            _context.OptionListItems.Add(oli);
        }
    }
}
