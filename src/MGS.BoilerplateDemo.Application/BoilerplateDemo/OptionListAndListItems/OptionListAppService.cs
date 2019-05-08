using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
using Abp.Application.Services;
using MGS.BoilerplateDemo.BoilerplateDemo.OptionListsAndListItems;
using MGS.BoilerplateDemo.BoilerplateDemo.OptionListAndListItems.Dtos;
using Abp.Authorization;
using MGS.BoilerplateDemo.Authorization;

namespace MGS.BoilerplateDemo.BoilerplateDemo.OptionListAndListItems
{
    [AbpAuthorize(PermissionNames.Pages_OptionLists, PermissionNames.Pages_Tenants_OptionLists)]
    public class OptionListAppService : AsyncCrudAppService<OptionList, OptionListViewDto, int, GetOptionListDto, OptionListCreateOrEditDto, OptionListCreateOrEditDto>, IOptionListAppService
    {
        private IRepository<OptionList> _optionListRepository;

        public OptionListAppService(IRepository<OptionList> optionListRepository) : base(optionListRepository)
        {
            _optionListRepository = optionListRepository;
        }

        /// <summary>
        /// Get all Lists by filter or all
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<OptionListViewDto>> GetAllLists(GetOptionListDto input)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                var tenantId = AbpSession.TenantId;  //is null if on host

                var filteredResults = await _optionListRepository.GetAll()
                                                //.Include(x => x.OptionListItems)
                                                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), ol =>
                                                            (ol.OptionListKey != null && ol.OptionListKey.Equals(input.Filter
                                                                                    , StringComparison.CurrentCultureIgnoreCase))
                                                            || (ol.DisplayName != null && ol.DisplayName.Equals(input.Filter
                                                                                    , StringComparison.CurrentCultureIgnoreCase))
                                                            || (ol.Description != null && ol.Description.Equals(input.Filter
                                                                                    , StringComparison.CurrentCultureIgnoreCase))
                                                        )
                                                .WhereIf(tenantId != null, x => x.TenantId == tenantId)
                                                .WhereIf(tenantId == null, x => x.TenantId == null).ToListAsync();

                var results = ObjectMapper.Map<List<OptionListViewDto>>(filteredResults);

                return new PagedResultDto<OptionListViewDto>(
                        results.Count
                        , results
                );
            }
        }

        /// <summary>
        /// Get List By Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OptionListViewDto> GetListByEntityId(EntityDto input)
        {
            return await GetListById(input.Id);
        }

        /// <summary>
        /// Get a list by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OptionListViewDto> GetListById(int id)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                var result = await _optionListRepository.GetAllIncluding(x => x.OptionListItems).FirstOrDefaultAsync(x => x.Id == id);
                return ObjectMapper.Map<OptionListViewDto>(result);
            }
        }

        /// <summary>
        /// Get a specific List by Key
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OptionListViewDto> GetListByKey(GetOptionListDto input)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                if (string.IsNullOrWhiteSpace(input.Key)) return null;

                var match = await _optionListRepository
                                        .GetAllIncluding(x => x.OptionListItems)
                                        .FirstOrDefaultAsync(x => x.OptionListKey.Equals(input.Key, StringComparison.CurrentCultureIgnoreCase));

                return ObjectMapper.Map<OptionListViewDto>(match);
            }
        }

        /// <summary>
        /// Create or Update a list
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_OptionLists_Create, PermissionNames.Pages_Tenants_OptionLists_Create
                        , PermissionNames.Pages_OptionLists_Update, PermissionNames.Pages_Tenants_OptionListItems_Update)]
        public async Task CreateOrUpdateList(OptionListCreateOrEditDto input)
        {
            if (input.Id <= 0)
            {
                await CreateList(input);
            }
            else
            {
                await UpdateList(input);
            }
        }

        /// <summary>
        /// Create a new list
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_OptionLists_Create, PermissionNames.Pages_Tenants_OptionLists_Create)]
        private async Task CreateList(OptionListCreateOrEditDto input)
        {
            var entry = ObjectMapper.Map<OptionList>(input);
            entry.TenantId = AbpSession.TenantId;  //null if on host, otherwise is tenant specific

            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                await _optionListRepository.InsertAsync(entry);
            }
        }

        /// <summary>
        /// Update a list
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_OptionLists_Update, PermissionNames.Pages_Tenants_OptionListItems_Update)]
        private async Task UpdateList(OptionListCreateOrEditDto input)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                var entry = await _optionListRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
                entry.TenantId = AbpSession.TenantId;  //null if on host, otherwise is tenant specific probably not nec on update.

                if (entry != null)
                {
                    ObjectMapper.Map(input, entry);
                }
            }
        }

        /// <summary>
        /// Delete a list by Entity Dto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_OptionLists_Delete, PermissionNames.Pages_Tenants_OptionLists_Delete)]
        public async Task DeleteListByEntity(EntityDto input)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                await DeleteList(input.Id);
            }
        }

        /// <summary>
        /// Delete a list by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_OptionLists_Delete, PermissionNames.Pages_Tenants_OptionLists_Delete)]
        public async Task DeleteList(int id)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                await _optionListRepository.DeleteAsync(x => x.Id == id);
            }
        }

        [AbpAuthorize(PermissionNames.Pages_OptionLists_Delete, PermissionNames.Pages_Tenants_OptionLists_Delete)]
        public async Task DeleteListItemsByList(string key)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                var match = await _optionListRepository
                                .GetAllIncluding(x => x.OptionListItems)
                                .FirstOrDefaultAsync(x => x.OptionListKey.Equals(key, StringComparison.CurrentCultureIgnoreCase));

                if (match != null)
                {
                    match.OptionListItems = new List<OptionListItem>();
                }
            }
        }
    }
}
