using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace MGS.BoilerplateDemo.Localization
{
    public static class BoilerplateDemoLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(BoilerplateDemoConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(BoilerplateDemoLocalizationConfigurer).GetAssembly(),
                        "MGS.BoilerplateDemo.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
