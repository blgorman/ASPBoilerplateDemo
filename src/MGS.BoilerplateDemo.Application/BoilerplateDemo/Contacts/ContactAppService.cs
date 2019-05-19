using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using MGS.BoilerplateDemo.Authorization;
using MGS.BoilerplateDemo.BoilerplateDemo.Contacts.Dtos;

namespace MGS.BoilerplateDemo.BoilerplateDemo.Contacts
{
    [AbpAuthorize(PermissionNames.Pages_Contacts)]
    public class ContactAppService : AsyncCrudAppService<Contact, ContactListViewDto, int, GetContactDto, ContactCreateOrEditDto, ContactCreateOrEditDto>, IContactAppService
    {
        public ContactAppService(IRepository<Contact, int> repository) : base(repository)
        {

        }

        [AbpAuthorize(PermissionNames.Pages_Contacts_Create)]
        public override Task<ContactListViewDto> Create(ContactCreateOrEditDto input)
        {
            return base.Create(input);
        }

        [AbpAuthorize(PermissionNames.Pages_Contacts_Delete)]
        public override Task Delete(EntityDto<int> input)
        {
            return base.Delete(input);
        }

        public override Task<ContactListViewDto> Get(EntityDto<int> input)
        {
            return base.Get(input);
        }

        public override Task<PagedResultDto<ContactListViewDto>> GetAll(GetContactDto input)
        {
            return base.GetAll(input);
        }

        [AbpAuthorize(PermissionNames.Pages_Contacts_Update)]
        public override Task<ContactListViewDto> Update(ContactCreateOrEditDto input)
        {
            return base.Update(input);
        }
    }
}
