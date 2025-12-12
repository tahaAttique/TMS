import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44370/',
  redirectUri: baseUrl,
  clientId: 'TMS_App',
  responseType: 'code',
  scope: 'offline_access TMS',
  requireHttps: true,
};

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'TMS',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44370',
      rootNamespace: 'TMS',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
  remoteEnv: {
    url: '/getEnvConfig',
    mergeStrategy: 'deepmerge'
  }
} as Environment;
