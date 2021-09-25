import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { SmartAdminUIComponent } from './components/smart-admin-uI.component';
import { SmartAdminUIRoutingModule } from './smart-admin-uI-routing.module';

@NgModule({
  declarations: [SmartAdminUIComponent],
  imports: [CoreModule, ThemeSharedModule, SmartAdminUIRoutingModule],
  exports: [SmartAdminUIComponent],
})
export class SmartAdminUIModule {
  static forChild(): ModuleWithProviders<SmartAdminUIModule> {
    return {
      ngModule: SmartAdminUIModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<SmartAdminUIModule> {
    return new LazyModuleFactory(SmartAdminUIModule.forChild());
  }
}
