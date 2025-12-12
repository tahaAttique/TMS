import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateTicketCategoryDto {
  name: string;
  description?: string;
}

export interface GetTicketCategoryListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
  name?: string;
  description?: string;
}

export interface TicketCategoryDto extends EntityDto<string> {
  name?: string;
  description?: string;
}

export interface UpdateTicketCategoryDto {
  name: string;
  description?: string;
}
