import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TicketComponent } from './ticket.component';

const routes: Routes = [{ path: '', component: TicketComponent }, { path: 'create-tickets', loadChildren: () => import('./create-ticket/create-ticket.module').then(m => m.CreateTicketModule) }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TicketRoutingModule { }
