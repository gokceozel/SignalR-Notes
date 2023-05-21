import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr"
import { PopulationChart} from '../models/PopulationChart';
import {ChartModel} from '../models/ChartModel';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  //data: PopulationChart[];
  public data: ChartModel[];
  public bradcastedData: ChartModel[];

  private hubConnection: signalR.HubConnection
    public startConnection = () => {
      this.hubConnection = new signalR.HubConnectionBuilder()
                              .withUrl('https://localhost:5001/PopulationHub')
                              .build();
      this.hubConnection
        .start()
        .then(() => console.log('Connection started'))
        .catch(err => console.log('Error while starting connection: ' + err))
    }
    
    // public addTransferChartDataListener = () => {
    //   this.hubConnection.on('ReceiveList', (data) => {
    //     console.log("add transfer Chart");
    //     this.data = data;
    //     console.log(data);
    //   });
    // }


    public addTransferChartDataListener = () => {
      this.hubConnection.on('transferchartdata', (data) => {
        this.data = data;
        console.log(data);
      });
    }

    public broadcastChartData = () => {
      const data = this.data.map(m => {
        const temp = {
          data: m.data,
          label: m.label
        }
        return temp;
      });

      this.hubConnection.invoke('broadcastchartdata', data)
      .catch(err => console.error(err));
    }

    public addBroadcastChartDataListener = () => {
      this.hubConnection.on('broadcastchartdata', (data) => {
        this.bradcastedData = data;
      })
    }

}
