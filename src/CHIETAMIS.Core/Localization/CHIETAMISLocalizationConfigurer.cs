using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace CHIETAMIS.Localization
{
    public static class CHIETAMISLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(CHIETAMISConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(CHIETAMISLocalizationConfigurer).GetAssembly(),
                        "CHIETAMIS.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
