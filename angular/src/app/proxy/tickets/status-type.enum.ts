import { mapEnumToOptions } from '@abp/ng.core';

export enum StatusType {
  New = 1,
  Open = 2,
  InProgress = 3,
  Resolved = 4,
  Closed = 5,
  Pending = 6,
  OnHold = 7,
  Canceled = 8,
}

export const statusTypeOptions = mapEnumToOptions(StatusType);
