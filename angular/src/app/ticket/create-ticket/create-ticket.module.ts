import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateTicketRoutingModule } from './create-ticket-routing.module';
import { CreateTicketComponent } from './create-ticket.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { QuillModule } from 'ngx-quill';
import { NzTabsModule } from 'ng-zorro-antd/tabs';


@NgModule({
  declarations: [
    CreateTicketComponent
  ],
  imports: [
    CommonModule,
    CreateTicketRoutingModule,
    SharedModule,
    NzSelectModule,
    QuillModule.forRoot(),
    QuillModule,
    NzTabsModule 
  ]
})
export class CreateTicketModule { }
