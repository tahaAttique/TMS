import * as signalR from '@microsoft/signalr';
import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.scss']
})
export class NotificationComponent {
  notificationList: string[] = [];
  connection!: signalR.HubConnection;

  constructor(private oAuthService: OAuthService) {
    this.initializeSignalRConnection();
  }

  initializeSignalRConnection() {
    debugger;
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(`https://localhost:44370/signalr-hubs/notification`, {
        accessTokenFactory: () => this.oAuthService.getAccessToken(),
      })
      .build();

    this.connection
      .start()
      .then(() => console.log('SignalR Connection Started'))
      .catch(err => console.error('SignalR Error:', err));

    this.connection.on('ReceiveNotification', (message: string) => {
      console.log('🔔 New Notification:', message);
      this.notificationList.push(message);
    });
  }

  clearNotifications() {
    this.notificationList = [];
  }
}
