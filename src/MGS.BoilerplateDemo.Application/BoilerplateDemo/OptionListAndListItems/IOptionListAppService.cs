using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MGS.BoilerplateDemo.BoilerplateDemo.OptionListAndListItems.Dtos;
using System.Threading.Tasks;

namespace MGS.BoilerplateDemo.BoilerplateDemo.OptionListAndListItems
{
    public interface IOptionListAppService : IAsyncCrudAppService<OptionListViewDto, int, GetOptionListDto, OptionListCreateOrEditDto, OptionListCreateOrEditDto>
    {
        //Get methods
        Task<PagedResultDto<OptionListViewDto>> GetAllLists(GetOptionListDto input);

        Task<OptionListViewDto> GetListByKey(GetOptionListDto input);

        Task<OptionListViewDto> GetListByEntityId(EntityDto input);

        Task<OptionListViewDto> GetListById(int id);

        //Create/Update
        Task CreateOrUpdateList(OptionListCreateOrEditDto input);

        //Delete
        Task DeleteListByEntity(EntityDto input);
        Task DeleteList(int id);

        Task DeleteListItemsByList(string key);
    }
}
