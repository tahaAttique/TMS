import { NavItemsService } from '@abp/ng.theme.shared';
import { AfterViewInit, Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  template: `
    <i class="fas fa-bell fa-lg text-white m-2" (click)="showNotifications()"></i>
  `,
})

export class MyNotificationComponent {
  constructor(private route: Router){}
  showNotifications() {
    this.route.navigate(['/notifications'])
  }
}

@Component({
  selector: 'app-root',
  template: `
    <abp-loader-bar></abp-loader-bar>
    <abp-dynamic-layout></abp-dynamic-layout>
    `
  ,
})
export class AppComponent {
  constructor(private navItems: NavItemsService) {
    
    navItems.addItems([
      {
        id: 'MyNotification',
        order: 2,
        component: MyNotificationComponent,
        requiredPolicy: 'TMS.Notifications'
      }
    ]);
  }
}