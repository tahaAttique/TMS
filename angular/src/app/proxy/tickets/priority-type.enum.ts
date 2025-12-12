import { mapEnumToOptions } from '@abp/ng.core';

export enum PriorityType {
  Low = 1,
  Meduim = 2,
  High = 3,
  Urgent = 4,
}

export const priorityTypeOptions = mapEnumToOptions(PriorityType);
