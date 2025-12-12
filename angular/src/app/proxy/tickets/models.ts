import type { PriorityType } from './priority-type.enum';
import type { StatusType } from './status-type.enum';
import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateTicketDto {
  title: string;
  description: string;
  expectedBehaviour?: string;
  actualBehaviour?: string;
  knownWorkRound?: string;
  stepsToReproduce?: string;
  operatingSystem: string;
  priorityType?: PriorityType;
  statusType?: StatusType;
  resolvedDate?: string;
  ticketCategoryId: string;
  userId?: string;
  assignedToUserId?: string;
  selfAssignedByUserId?: string;
}

export interface CurrentUserLookUpDto {
  userId?: string;
  name?: string;
}

export interface GetTicketListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
  title?: string;
  description?: string;
  expectedBehaviour?: string;
  actualBehaviour?: string;
  nonWorkRound?: string;
  operatingSystem?: string;
  priorityType?: PriorityType;
  statusType?: StatusType;
  resolvedDate?: string;
  ticketCategoryId?: string;
  userId?: string;
  assignedToUserId?: string;
  selfAssignedByUserId?: string;
}

export interface KeyLookUpDto {
  id?: string;
  userName?: string;
}

export interface TicketCategoryLookUpDto extends EntityDto<string> {
  name?: string;
}

export interface TicketDto extends EntityDto<string> {
  title?: string;
  description?: string;
  expectedBehaviour?: string;
  actualBehaviour?: string;
  knownWorkRound?: string;
  stepsToReproduce?: string;
  operatingSystem?: string;
  priorityType?: PriorityType;
  statusType?: StatusType;
  resolvedDate?: string;
  ticketCategoryId?: string;
  userId?: string;
  userName?: string;
  assignedToUserId?: string;
  assignedUserName?: string;
  selfAssignedByUserId?: string;
  selfAssignedUserName?: string;
  ticketCategory?: string;
  createdDate?: string;
}

export interface UpdateTicketDto {
  title: string;
  description: string;
  expectedBehaviour?: string;
  actualBehaviour?: string;
  knownWorkRound?: string;
  stepsToReproduce?: string;
  operatingSystem: string;
  priorityType?: PriorityType;
  statusType?: StatusType;
  resolvedDate?: string;
  ticketCategoryId: string;
  userId?: string;
  assignedToUserId?: string;
  selfAssignedByUserId?: string;
}
