import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { environment } from 'src/environments/environment';
import { Subject } from 'rxjs';

const API_BASE_URL = environment.API_BASE_URL;

@Injectable({
  providedIn: 'root'
})
export class WebsocketService {
  public lockOperationsSubject = new Subject<any>();

  private _hubConnection: HubConnection;

  constructor() {
    const builder = new HubConnectionBuilder();

    this._hubConnection = builder.withUrl(API_BASE_URL + '/Locks').build();

    this._hubConnection.start().then( result =>
        console.log("Hub connection established")
    );

    this._hubConnection.on('SetLockState', (lockId: number, locked: boolean) => {
      this.lockOperationsSubject.next({lockId: lockId, locked: locked});
    });
  }
}