import type { CommentDto, CreateCommentDto, GetCommentListDto, UpdateCommentDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { TicketDto } from '../tickets/models';
import type { StatusType } from '../tickets/status-type.enum';

@Injectable({
  providedIn: 'root',
})
export class CommentService {
  apiName = 'Default';
  

  create = (input: CreateCommentDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CommentDto>({
      method: 'POST',
      url: '/api/app/comments',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/comments/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CommentDto>({
      method: 'GET',
      url: `/api/app/comments/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getCommentsByTicketId = (ticketId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CommentDto[]>({
      method: 'GET',
      url: `/api/app/comments/get-comment-list-by-ticketId/${ticketId}`,
    },
    { apiName: this.apiName,...config });
  

  getDetailOfTicket = (ticketId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TicketDto>({
      method: 'GET',
      url: `/api/app/comments/ticketDetail/${ticketId}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetCommentListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CommentDto>>({
      method: 'GET',
      url: '/api/app/comments',
      params: { filter: input.filter, detail: input.detail, ticketId: input.ticketId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateCommentDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/comments/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  updateStatus = (ticketId: string, statusType: StatusType, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/comments/update-status/${ticketId}`,
      params: { statusType },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
