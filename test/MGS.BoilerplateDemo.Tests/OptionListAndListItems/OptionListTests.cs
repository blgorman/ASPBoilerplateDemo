using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Shouldly;
using MGS.BoilerplateDemo.BoilerplateDemo.OptionListAndListItems;
using MGS.BoilerplateDemo.BoilerplateDemo.OptionListAndListItems.Dtos;


namespace MGS.BoilerplateDemo.Tests.OptionListAndListItems
{
    public class OptionListTests : BoilerplateDemoTestBase
    {
        IOptionListAppService _optionListService;

        public OptionListTests()
        {
            _optionListService = Resolve<IOptionListAppService>();
        }

        [Theory()]
        [InlineData("asdf", 0)]
        [InlineData("states", 1)]
        [InlineData("", 2)]
        public async Task TestGetAllLists(string filter, int expectedCount)
        {
            //arrange
            var input = new GetOptionListDto()
            {
                Filter = filter
            };

            await UsingDbContextAsync(async context => {
                var lists = await context.OptionLists.Where(e => CultureInfo.InvariantCulture.CompareInfo.IndexOf(e.DisplayName, filter, CompareOptions.IgnoreCase) >= 0).ToListAsync();
                lists.ShouldNotBeNull();
                lists.Count.ShouldBe(expectedCount);
            });
        }
    }
}
