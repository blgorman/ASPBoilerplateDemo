using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGS.BoilerplateDemo.BoilerplateDemo.Contacts.Dtos
{
    public class GetContactDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string Key { get; set; }
    }
}
