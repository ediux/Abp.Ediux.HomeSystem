import { ModuleWithProviders, NgModule } from '@angular/core';
import { SMART_ADMIN_UI_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class SmartAdminUIConfigModule {
  static forRoot(): ModuleWithProviders<SmartAdminUIConfigModule> {
    return {
      ngModule: SmartAdminUIConfigModule,
      providers: [SMART_ADMIN_UI_ROUTE_PROVIDERS],
    };
  }
}
