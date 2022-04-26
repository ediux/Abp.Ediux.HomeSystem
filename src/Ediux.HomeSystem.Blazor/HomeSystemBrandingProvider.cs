using Ediux.HomeSystem.Localization;

using Microsoft.Extensions.Localization;

using System;

using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Ediux.HomeSystem.Blazor;

[Dependency(ReplaceServices = true)]
public class HomeSystemBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer _localizer;

    private Type _localizationResource = typeof(HomeSystemResource);

    [Dependency]
    public IAbpLazyServiceProvider LazyServiceProvider
    {
        get;
        set;
    }

    protected IStringLocalizerFactory StringLocalizerFactory => LazyServiceProvider.LazyGetRequiredService<IStringLocalizerFactory>();

    protected IStringLocalizer L
    {
        get
        {
            if (_localizer == null)
            {
                _localizer = CreateLocalizer();
            }

            return _localizer;
        }
    }

    protected Type LocalizationResource
    {
        get
        {
            return _localizationResource;
        }
        set
        {
            _localizationResource = value;
            _localizer = null;
        }
    }

    protected virtual IStringLocalizer CreateLocalizer()
    {
        if (LocalizationResource != null)
        {
            return StringLocalizerFactory.Create(LocalizationResource);
        }

        return StringLocalizerFactory.CreateDefaultOrNull() ?? throw new AbpException("Set LocalizationResource or define the default localization resource type (by configuring the AbpLocalizationOptions.DefaultResourceType) to be able to use the L object!");
    }

    public override string AppName => L[HomeSystemResource.Common.SiteName];
}
