import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TicketCategoryRoutingModule } from './ticket-category-routing.module';
import { TicketCategoryComponent } from './ticket-category.component';
import { SharedModule } from '../shared/shared.module';
import { PageModule } from '@abp/ng.components/page';


@NgModule({
  declarations: [
    TicketCategoryComponent
  ],
  imports: [
    CommonModule,
    TicketCategoryRoutingModule,
    SharedModule,
    PageModule,
  ]
})
export class TicketCategoryModule { }
