import { authGuard, permissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: '/tickets',
    // loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
  },
  {
    path: 'account',
    loadChildren: () => import('@abp/ng.account').then(m => m.AccountModule.forLazy()),
  },
  {
    path: 'identity',
    loadChildren: () => import('@abp/ng.identity').then(m => m.IdentityModule.forLazy()),
  },
  {
    path: 'tenant-management',
    loadChildren: () =>
      import('@abp/ng.tenant-management').then(m => m.TenantManagementModule.forLazy()),
  },
  {
    path: 'setting-management',
    loadChildren: () =>
      import('@abp/ng.setting-management').then(m => m.SettingManagementModule.forLazy()),
  },
  { path: 'tickets', loadChildren: () => import('./ticket/ticket.module').then(m => m.TicketModule) },
  { path: 'ticket-categories', loadChildren: () => import('./ticket-category/ticket-category.module').then(m => m.TicketCategoryModule), canActivate: [authGuard, permissionGuard] },
  { path: 'create-ticket', loadChildren: () => import('./ticket/create-ticket/create-ticket.module').then(m => m.CreateTicketModule), canActivate: [authGuard, permissionGuard] },
  { path: 'comments/:id', loadChildren: () => import('./comment/comment.module').then(m => m.CommentModule) },
  { path: 'notifications', loadChildren: () => import('./notification/notification.module').then(m => m.NotificationModule), canActivate: [authGuard, permissionGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {})],
  exports: [RouterModule],
})
export class AppRoutingModule {}
