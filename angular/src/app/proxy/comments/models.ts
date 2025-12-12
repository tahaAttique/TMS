import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CommentDto extends EntityDto<string> {
  detail?: string;
  ticketId?: string;
  userId?: string;
  userName?: string;
}

export interface CreateCommentDto {
  detail: string;
  ticketId: string;
}

export interface GetCommentListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
  detail?: string;
  ticketId?: string;
}

export interface UpdateCommentDto {
  detail: string;
  ticketId: string;
}
