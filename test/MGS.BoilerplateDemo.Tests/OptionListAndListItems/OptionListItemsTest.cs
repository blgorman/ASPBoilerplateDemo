using Abp.Application.Services.Dto;
using MGS.BoilerplateDemo.BoilerplateDemo.OptionListAndListItems;
using MGS.BoilerplateDemo.BoilerplateDemo.OptionListAndListItems.Dtos;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MGS.BoilerplateDemo.Tests.OptionListAndListItems
{
    public class OptionListItemsTest : BoilerplateDemoTestBase
    {

        IOptionListAppService _optionListService;
        IOptionListItemsAppService _optionListItemService;

        public OptionListItemsTest()
        {
            LoginAsHostAdmin();
            _optionListService = Resolve<IOptionListAppService>();
            _optionListItemService = Resolve<IOptionListItemsAppService>();
        }

        //GetAll by filter
        [Theory]
        [InlineData("asdf", 0)]
        [InlineData("AL", 1)]
        [InlineData("Iowa", 1)]
        [InlineData("Oran", 1)]
        [InlineData("", 5)]
        public async Task TestGetAllByFilter(string filter, int expectedCount)
        {
            //arrange
            var input = new GetOptionListItemDto()
            {
                Filter = filter
            };

            //act
            var results = await _optionListItemService.GetAllOptionListItems(input);

            //assert
            results.ShouldNotBeNull();
            results.Items.Count.ShouldBe(expectedCount);
        }

        //GetListItemByIdEntityDto
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(0, 0)]
        [InlineData(-1, 0)]
        [InlineData(9876, 0)]
        public async Task TestGetListItemByIdEntityDto(int id, int expectedId)
        {
            //arrange
            var input = new EntityDto()
            {
                Id = id
            };

            //act
            var result = await _optionListItemService.GetListItemByEntityId(input);

            //assert
            if (expectedId <= 0 || expectedId > 9800)
            {
                result.ShouldBeNull();
            }
            else
            {
                result.ShouldNotBeNull();
                result.Id.ShouldBe(expectedId);
            }
        }

        //GetListItemByIdInt
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(0, 0)]
        [InlineData(-1, 0)]
        [InlineData(9876, 0)]
        public async Task TestGetListItemByIdInt(int id, int expectedId)
        {
            //act
            var result = await _optionListItemService.GetListItemById(id);

            //assert
            if (expectedId <= 0 || expectedId > 9800)
            {
                result.ShouldBeNull();
            }
            else
            {
                result.ShouldNotBeNull();
                result.Id.ShouldBe(expectedId);
            }
        }

        //GetListItemsByListKey
        [Theory]
        [InlineData("States", 2)]
        [InlineData("colors", 3)]
        [InlineData("planets", 0)]
        [InlineData("", 0)]
        public async Task TestGetAllByListKey(string listKey, int expectedCount)
        {
            //arrange
            var input = new GetOptionListItemDto()
            {
                Key = listKey
            };

            //act
            var results = await _optionListItemService.GetListItemsByListKey(input);

            //assert
            results.ShouldNotBeNull();
            results.Items.ShouldNotBeNull();
            results.Items.Count.ShouldBe(expectedCount);
        }

        private const string TEST_LIST_DESCRIPTION = "A list description";
        private const string TEST_LIST_DISPLAYNAME = "TestList";
        private const string TEST_LIST_KEY = "TESTLIST";

        private const string TEST_DISPLAY_TEXT = "List Item Test";
        private const string TEST_DISPLAY_TEXT_UPDATED = "New Item Text";
        private const string TEST_ADDITIONAL_INFO = "Info Text";
        private const string TEST_ADDITIONAL_INFO_UPDATED = "Updated Info Text";
        private const int TEST_DISPLAY_ORDER = 10;
        private const int TEST_DISPLAY_ORDER_UPDATED = 20;

        private OptionListCreateOrEditDto GetAListForTesting()
        {
            return new OptionListCreateOrEditDto()
            {
                Description = TEST_LIST_DESCRIPTION,
                DisplayName = TEST_LIST_DISPLAYNAME,
                IsActive = true,
                OptionListKey = TEST_LIST_KEY,
                TenantId = null,
                OptionListItems = null,
            };
        }

        private OptionListItemCreateOrEditDto GetAListItemForTesting(int listId)
        {
            return new OptionListItemCreateOrEditDto()
            {
                AdditionalInfo = TEST_ADDITIONAL_INFO,
                DisplayOrder = TEST_DISPLAY_ORDER,
                DisplayText = TEST_DISPLAY_TEXT,
                IsActive = true,
                OptionListId = listId,
            };
        }

        private const string EXISTING_LIST_KEY = "States";

        //CreateListItem
        [Fact]
        public async Task TestCreateListItem()
        {
            //arrange
            var theList = GetAListForTesting();
            await _optionListService.CreateOrUpdateList(theList);
            var theResult = await _optionListService.GetListByKey(new GetOptionListDto() { Key = TEST_LIST_KEY });
            theResult.ShouldNotBeNull();

            var theItem = GetAListItemForTesting(theResult.Id);
            await _optionListItemService.CreateOrUpdateListItem(theItem);

            await UsingDbContextAsync(async context => {


                var theNewResult = await _optionListService.GetListByKey(new GetOptionListDto() { Key = TEST_LIST_KEY });
                var items = await context.OptionListItems.Include(y => y.OptionList).Where(e => e.OptionListId == theItem.OptionListId).ToListAsync();
                items.ShouldNotBeNull();
                items.Count.ShouldBeGreaterThanOrEqualTo(1);
                var theAddedItem = items.FirstOrDefault(x => x.DisplayText == TEST_DISPLAY_TEXT);
                theAddedItem.AdditionalInfo.ShouldBe(TEST_ADDITIONAL_INFO);
                theAddedItem.DisplayOrder.ShouldBe(TEST_DISPLAY_ORDER);
            });
        }

        private const string COLORS_LIST = "Colors";

        //UpdateListItem
        [Fact]
        public async Task TestUpdateListItem()
        {
            //update an existing entry 
            //arrange
            var theList = await _optionListService.GetListByKey(new GetOptionListDto() { Key = COLORS_LIST });
            theList.ShouldNotBeNull();
            theList.OptionListItems.Count.ShouldBeGreaterThanOrEqualTo(3);

            var theItem = theList.OptionListItems.FirstOrDefault(x => x.DisplayText.Equals("yellow", StringComparison.CurrentCultureIgnoreCase));

            theItem.DisplayOrder = TEST_DISPLAY_ORDER_UPDATED;
            theItem.DisplayText = TEST_DISPLAY_TEXT_UPDATED;
            theItem.AdditionalInfo = TEST_ADDITIONAL_INFO_UPDATED;

            var theItemAsCorEDto = new OptionListItemCreateOrEditDto()
            {
                AdditionalInfo = theItem.AdditionalInfo,
                IsActive = theItem.IsActive,
                OptionListId = theList.Id,
                DisplayOrder = theItem.DisplayOrder,
                DisplayText = theItem.DisplayText,
                Id = theItem.Id
            };

            await _optionListItemService.CreateOrUpdateListItem(theItemAsCorEDto);

            await UsingDbContextAsync(async context => {
                var theNewResult = await _optionListService.GetListByKey(new GetOptionListDto() { Key = COLORS_LIST });
                var items = await context.OptionListItems.Include(y => y.OptionList).Where(e => e.OptionListId == theItem.OptionListId).ToListAsync();

                var allItems = await context.OptionListItems.ToListAsync();

                items.ShouldNotBeNull();
                items.Count.ShouldBeGreaterThanOrEqualTo(1);
                var theUpdatedItem = items.FirstOrDefault(x => x.DisplayText == TEST_DISPLAY_TEXT_UPDATED);
                theUpdatedItem.AdditionalInfo.ShouldBe(TEST_ADDITIONAL_INFO_UPDATED);
                theUpdatedItem.DisplayOrder.ShouldBe(TEST_DISPLAY_ORDER_UPDATED);
            });
        }

        //DeleteAllListItemsByListKey
        [Fact]
        public async Task TestDeleteAllListItemsByKey()
        {
            //arrange
            var theList = await _optionListService.GetListByKey(new GetOptionListDto() { Key = COLORS_LIST });
            theList.ShouldNotBeNull();
            theList.OptionListItems.Count.ShouldBeGreaterThanOrEqualTo(3);

            //act
            await _optionListItemService.DeleteAllListItemsByListKey(COLORS_LIST);

            await UsingDbContextAsync(async context => {
                var theNewResult = await _optionListService.GetListByKey(new GetOptionListDto() { Key = COLORS_LIST });
                var items = await context.OptionListItems.Include(y => y.OptionList).Where(e => e.OptionListId == theList.Id).ToListAsync();
                items.ShouldNotBeNull();
                items.Count.ShouldBe(0);
            });
        }

        //DeleteListItemEntityDto
        [Fact]
        public async Task TestDeleteListItemByEntityDto()
        {
            //arrange
            var theList = await _optionListService.GetListByKey(new GetOptionListDto() { Key = COLORS_LIST });
            theList.ShouldNotBeNull();
            theList.OptionListItems.Count.ShouldBeGreaterThanOrEqualTo(3);

            var theItem = theList.OptionListItems.FirstOrDefault(x => x.DisplayText.Equals("yellow", StringComparison.CurrentCultureIgnoreCase));
            theItem.IsDeleted.ShouldBeFalse();

            //act
            await _optionListItemService.DeleteListItemByEntity(new EntityDto { Id = theItem.Id });

            //assert
            await UsingDbContextAsync(async context => {
                var theNewResult = await _optionListService.GetListByKey(new GetOptionListDto() { Key = COLORS_LIST });
                var item = await context.OptionListItems.Include(y => y.OptionList).FirstOrDefaultAsync(e => e.Id == theItem.Id);

                item.IsDeleted.ShouldBeTrue();
            });
        }

        //DeleteListItemInt
        [Fact]
        public async Task TestDeleteListItemByIntId()
        {
            //arrange
            var theList = await _optionListService.GetListByKey(new GetOptionListDto() { Key = COLORS_LIST });
            theList.ShouldNotBeNull();
            theList.OptionListItems.Count.ShouldBeGreaterThanOrEqualTo(3);

            var theItem = theList.OptionListItems.FirstOrDefault(x => x.DisplayText.Equals("yellow", StringComparison.CurrentCultureIgnoreCase));
            theItem.IsDeleted.ShouldBeFalse();

            //act
            await _optionListItemService.DeleteListItem(theItem.Id);

            //assert
            await UsingDbContextAsync(async context => {
                var theNewResult = await _optionListService.GetListByKey(new GetOptionListDto() { Key = COLORS_LIST });
                var item = await context.OptionListItems.Include(y => y.OptionList).FirstOrDefaultAsync(e => e.Id == theItem.Id);

                item.IsDeleted.ShouldBeTrue();
            });
        }
    }
}
