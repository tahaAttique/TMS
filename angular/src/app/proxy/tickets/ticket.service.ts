import type { CreateTicketDto, CurrentUserLookUpDto, GetTicketListDto, KeyLookUpDto, TicketCategoryLookUpDto, TicketDto, UpdateTicketDto } from './models';
import type { PriorityType } from './priority-type.enum';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TicketService {
  apiName = 'Default';
  

  assignTicketToUser = (ticketId: string, assignedToId: string, priorityType: PriorityType, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/tickets/assign-ticket',
      params: { ticketId, assignedToId, priorityType },
    },
    { apiName: this.apiName,...config });
  

  create = (input: CreateTicketDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TicketDto>({
      method: 'POST',
      url: '/api/app/tickets',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/tickets/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TicketDto>({
      method: 'GET',
      url: `/api/app/tickets/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getAdminKeyLookUpDto = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, KeyLookUpDto[]>({
      method: 'GET',
      url: '/api/app/tickets/get-admin',
    },
    { apiName: this.apiName,...config });
  

  getAgentKeyUpDto = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, KeyLookUpDto[]>({
      method: 'GET',
      url: '/api/app/tickets/get-agent',
    },
    { apiName: this.apiName,...config });
  

  getCurrentUser = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, CurrentUserLookUpDto>({
      method: 'GET',
      url: '/api/app/tickets/get-current-user',
    },
    { apiName: this.apiName,...config });
  

  getCustomerKeyLookUpDto = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, KeyLookUpDto[]>({
      method: 'GET',
      url: '/api/app/tickets/get-customer',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetTicketListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<TicketDto>>({
      method: 'GET',
      url: '/api/app/tickets',
      params: { filter: input.filter, title: input.title, description: input.description, expectedBehaviour: input.expectedBehaviour, actualBehaviour: input.actualBehaviour, nonWorkRound: input.nonWorkRound, operatingSystem: input.operatingSystem, priorityType: input.priorityType, statusType: input.statusType, resolvedDate: input.resolvedDate, ticketCategoryId: input.ticketCategoryId, userId: input.userId, assignedToUserId: input.assignedToUserId, selfAssignedByUserId: input.selfAssignedByUserId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getTicketCategoryLookUp = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, TicketCategoryLookUpDto[]>({
      method: 'GET',
      url: '/api/app/tickets/get-ticket-category',
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateTicketDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/tickets/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
