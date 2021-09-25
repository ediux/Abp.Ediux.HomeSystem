import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'SmartAdminUI',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44384',
    redirectUri: baseUrl,
    clientId: 'SmartAdminUI_App',
    responseType: 'code',
    scope: 'offline_access SmartAdminUI role email openid profile',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44384',
      rootNamespace: 'Ediux.ABP.Features.SmartAdminUI',
    },
    SmartAdminUI: {
      url: 'https://localhost:44349',
      rootNamespace: 'Ediux.ABP.Features.SmartAdminUI',
    },
  },
} as Environment;
