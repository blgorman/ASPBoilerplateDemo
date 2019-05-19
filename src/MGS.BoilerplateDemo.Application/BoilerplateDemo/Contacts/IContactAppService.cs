using Abp.Application.Services;
using MGS.BoilerplateDemo.BoilerplateDemo.Contacts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGS.BoilerplateDemo.BoilerplateDemo.Contacts
{
    public interface IContactAppService : IAsyncCrudAppService<ContactListViewDto, int, GetContactDto, ContactCreateOrEditDto, ContactCreateOrEditDto>
    {
        
    }
}
