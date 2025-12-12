import type { CreateTicketCategoryDto, GetTicketCategoryListDto, TicketCategoryDto, UpdateTicketCategoryDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TicketCategoryService {
  apiName = 'Default';
  

  create = (input: CreateTicketCategoryDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TicketCategoryDto>({
      method: 'POST',
      url: '/api/app/ticket-categories',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/ticket-categories/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TicketCategoryDto>({
      method: 'GET',
      url: `/api/app/ticket-categories/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetTicketCategoryListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<TicketCategoryDto>>({
      method: 'GET',
      url: '/api/app/ticket-categories',
      params: { filter: input.filter, name: input.name, description: input.description, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateTicketCategoryDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/ticket-categories/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
