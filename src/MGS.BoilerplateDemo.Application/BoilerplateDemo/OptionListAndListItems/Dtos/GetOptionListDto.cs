using Abp.Application.Services.Dto;

namespace MGS.BoilerplateDemo.BoilerplateDemo.OptionListAndListItems.Dtos
{
    public class GetOptionListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string Key { get; set; }
    }
}
