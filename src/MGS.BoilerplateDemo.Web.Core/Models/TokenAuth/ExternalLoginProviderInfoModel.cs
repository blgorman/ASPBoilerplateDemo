using Abp.AutoMapper;
using MGS.BoilerplateDemo.Authentication.External;

namespace MGS.BoilerplateDemo.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
