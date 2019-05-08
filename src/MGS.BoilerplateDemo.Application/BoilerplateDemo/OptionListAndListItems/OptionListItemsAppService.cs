using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Collections.Extensions;
using Abp.Linq.Extensions;
using Abp.Domain.Uow;
using System.Linq;
using Abp.Application.Services;
using MGS.BoilerplateDemo.BoilerplateDemo.OptionListsAndListItems;
using MGS.BoilerplateDemo.BoilerplateDemo.OptionListAndListItems.Dtos;
using Abp.Authorization;
using MGS.BoilerplateDemo.Authorization;

namespace MGS.BoilerplateDemo.BoilerplateDemo.OptionListAndListItems
{
    [AbpAuthorize(PermissionNames.Pages_OptionListItems, PermissionNames.Pages_Tenants_OptionListItems)]
    public class OptionListItemsAppService : AsyncCrudAppService<OptionListItem, OptionListItemViewDto, int, GetOptionListItemDto, OptionListItemCreateOrEditDto, OptionListItemCreateOrEditDto>, IOptionListItemsAppService
    {
        private IOptionListAppService _optionListAppService;
        private IRepository<OptionListItem> _optionListItemRepository;

        public OptionListItemsAppService(IOptionListAppService optionListAppService,
                                            IRepository<OptionListItem> optionListItemRepository) : base(optionListItemRepository)
        {
            _optionListAppService = optionListAppService;
            _optionListItemRepository = optionListItemRepository;
        }

        /// <summary>
        /// Get All List Items by filter or list key
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<OptionListItemViewDto>> GetAllOptionListItems(GetOptionListItemDto input)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                //limit to the tenant or the host, but don't show both:
                var tenantId = AbpSession.TenantId; //is null if on host

                var filteredResults = await _optionListItemRepository.GetAll()
                                                    //.Include(x => x.OptionList)
                                                    .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                                                        oli => oli.DisplayText != null && oli.DisplayText.Contains(input.Filter
                                                                    , StringComparison.CurrentCultureIgnoreCase)
                                                        || oli.AdditionalInfo != null && oli.AdditionalInfo.Contains(input.Filter
                                                                        , StringComparison.CurrentCultureIgnoreCase))
                                                    .WhereIf(tenantId != null, x => x.OptionList.TenantId == tenantId)
                                                    .WhereIf(tenantId == null, x => x.OptionList.TenantId == null).ToListAsync();
                var results = ObjectMapper.Map<List<OptionListItemViewDto>>(filteredResults);
                return new PagedResultDto<OptionListItemViewDto>(
                        results.Count,
                        results
                    );
            }
        }

        public async Task<OptionListItemViewDto> GetListItemByEntityId(EntityDto input)
        {
            return await GetListItemById(input.Id);
        }

        public async Task<OptionListItemViewDto> GetListItemById(int id)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                var result = await _optionListItemRepository.FirstOrDefaultAsync(x => x.Id == id);
                return ObjectMapper.Map<OptionListItemViewDto>(result);
            }
        }

        private async Task<OptionListViewDto> GetFirstMatchingOptionListByKey(GetOptionListDto input)
        {
            if (string.IsNullOrEmpty(input.Key)) return null;
            return await _optionListAppService.GetListByKey(input);
        }

        public async Task<PagedResultDto<OptionListItemViewDto>> GetListItemsByListKey(GetOptionListItemDto input)
        {
            //here we don't want to limit: I need to see list items in a dropdown
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                if (string.IsNullOrEmpty(input.Key) && string.IsNullOrEmpty(input.Filter)) return new PagedResultDto<OptionListItemViewDto>(0
                            , new List<OptionListItemViewDto>());

                var theList = await GetFirstMatchingOptionListByKey(new GetOptionListDto() { Key = input.Key });

                if (theList != null && theList.OptionListItems != null && theList.OptionListItems.Any())
                {
                    var results = ObjectMapper.Map<List<OptionListItemViewDto>>(theList.OptionListItems);
                    return new PagedResultDto<OptionListItemViewDto>(
                            results.Count
                            , results
                    );
                }

                //return empty results:
                return new PagedResultDto<OptionListItemViewDto>(0
                            , new List<OptionListItemViewDto>());
            }
        }

        [AbpAuthorize(PermissionNames.Pages_OptionListItems_Create, PermissionNames.Pages_Tenants_OptionListItems_Create,
                        PermissionNames.Pages_OptionListItems_Update, PermissionNames.Pages_Tenants_OptionListItems_Update)]
        public async Task CreateOrUpdateListItem(OptionListItemCreateOrEditDto input)
        {
            if (input.Id <= 0)
            {
                await CreateListItem(input);
            }
            else
            {
                await UpdateListItem(input);
            }
        }

        [AbpAuthorize(PermissionNames.Pages_OptionListItems_Create, PermissionNames.Pages_Tenants_OptionListItems_Create)]
        private async Task CreateListItem(OptionListItemCreateOrEditDto input)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                var entry = ObjectMapper.Map<OptionListItem>(input);

                var list = await _optionListAppService.GetListById(input.OptionListId);
                if (list.OptionListItems.Any())
                {
                    foreach (var oli in list.OptionListItems)
                    {
                        if (oli.DisplayText.Equals(input.DisplayText, StringComparison.CurrentCultureIgnoreCase))
                        {
                            throw new Exception("List already contains entry - no list item created");
                        }
                    }
                }

                var theList = ObjectMapper.Map<OptionList>(list);
                entry.OptionList = theList;
                await _optionListItemRepository.InsertAsync(entry);

                theList.OptionListItems.Add(entry);
            }
        }

        [AbpAuthorize(PermissionNames.Pages_OptionListItems_Update, PermissionNames.Pages_Tenants_OptionListItems_Update)]
        private async Task UpdateListItem(OptionListItemCreateOrEditDto input)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                var entry = await _optionListItemRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
                if (entry != null)
                {
                    ObjectMapper.Map(input, entry);
                }
            }
        }

        [AbpAuthorize(PermissionNames.Pages_OptionListItems_Delete, PermissionNames.Pages_Tenants_OptionListItems_Delete)]
        public async Task DeleteAllListItemsByListKey(string key)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                await _optionListAppService.DeleteListItemsByList(key);
            }
        }

        [AbpAuthorize(PermissionNames.Pages_OptionListItems_Delete, PermissionNames.Pages_Tenants_OptionListItems_Delete)]
        public async Task DeleteListItemByEntity(EntityDto input)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                await DeleteListItem(input.Id);
            }
        }

        [AbpAuthorize(PermissionNames.Pages_OptionListItems_Delete, PermissionNames.Pages_Tenants_OptionListItems_Delete)]
        public async Task DeleteListItem(int id)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                await _optionListItemRepository.DeleteAsync(x => x.Id == id);
            }
        }
    }
}
