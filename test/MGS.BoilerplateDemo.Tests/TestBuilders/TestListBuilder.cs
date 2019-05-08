using MGS.BoilerplateDemo.BoilerplateDemo.OptionListsAndListItems;
using MGS.BoilerplateDemo.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGS.BoilerplateDemo.Tests.TestBuilders
{
    public class TestListBuilder
    {
        private readonly BoilerplateDemoDbContext _context;

        public TestListBuilder(BoilerplateDemoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLists();
        }

        public const string COLORS_LIST = "Colors";
        public const string STATES_LIST = "States";
        private void CreateLists()
        {
            var ol = CreateList(true, STATES_LIST, STATES_LIST, "States of the U.S.");
            CreateListItems(true, ol.Id, "Alabama", "AL");
            CreateListItems(true, ol.Id, "Iowa", "IA");

            ol = CreateList(true, COLORS_LIST, COLORS_LIST, "Another List");
            CreateListItems(true, ol.Id, "Red", "Red");
            CreateListItems(true, ol.Id, "Orange", "Orange");
            CreateListItems(true, ol.Id, "Yellow", "Yellow");
        }

        private OptionList CreateList(bool isActive, string key, string displayName, string description)
        {
            var optionList = new OptionList()
            {
                OptionListKey = key,
                IsActive = isActive,
                DisplayName = displayName,
                Description = description
            };

            var ctx = _context.OptionLists.Add(optionList).Entity;
            _context.SaveChanges();
            return ctx;
        }

        private OptionListItem CreateListItems(bool isActive, int listId, string displayText, string additionalInfo)
        {
            var optionListItem = new OptionListItem()
            {
                DisplayText = displayText,
                AdditionalInfo = additionalInfo,
                OptionListId = listId,
                IsActive = isActive
            };
            var ctx = _context.OptionListItems.Add(optionListItem).Entity;
            _context.SaveChanges();
            return ctx;
        }
    }
}
