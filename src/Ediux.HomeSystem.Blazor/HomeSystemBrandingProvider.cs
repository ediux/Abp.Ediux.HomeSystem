﻿using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Ediux.HomeSystem.Blazor;

[Dependency(ReplaceServices = true)]
public class HomeSystemBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "HomeSystem";
}
