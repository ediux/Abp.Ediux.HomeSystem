import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class SmartAdminUIService {
  apiName = 'SmartAdminUI';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/SmartAdminUI/sample' },
      { apiName: this.apiName }
    );
  }
}
