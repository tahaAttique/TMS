import { RoutesService, eLayoutType } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      // {
      //   path:'/tickets',
      //   name:'::Menu:Tickets',
      //   iconClass: 'fas fa-ticket-alt',
      //   order:2,
      //   layout: eLayoutType.application,
      //   requiredPolicy: 'TMS.Tickets'
      // },
      {
        path:'/ticket-categories',
        name:'::Menu:Ticket-Categories',
        iconClass:'fas fa-list',
        order:3,
        layout: eLayoutType.application,
        requiredPolicy: 'TMS.TicketCategories'
      },
    ]);
  };
}
