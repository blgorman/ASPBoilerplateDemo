using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MGS.BoilerplateDemo.BoilerplateDemo.OptionListAndListItems.Dtos;
using System.Threading.Tasks;

namespace MGS.BoilerplateDemo.BoilerplateDemo.OptionListAndListItems
{
    public interface IOptionListItemsAppService : IAsyncCrudAppService<OptionListItemViewDto, int, GetOptionListItemDto, OptionListItemCreateOrEditDto, OptionListItemCreateOrEditDto>
    {
        //Get
        //Get methods
        Task<PagedResultDto<OptionListItemViewDto>> GetAllOptionListItems(GetOptionListItemDto input);

        Task<PagedResultDto<OptionListItemViewDto>> GetListItemsByListKey(GetOptionListItemDto input);

        Task<OptionListItemViewDto> GetListItemByEntityId(EntityDto input);

        Task<OptionListItemViewDto> GetListItemById(int id);

        //Create/Update
        Task CreateOrUpdateListItem(OptionListItemCreateOrEditDto input);

        //Delete
        Task DeleteAllListItemsByListKey(string key);
        Task DeleteListItemByEntity(EntityDto input);
        Task DeleteListItem(int id);
    }
}
