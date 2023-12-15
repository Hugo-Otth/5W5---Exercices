import { Component } from '@angular/core';
import * as signalR from "@microsoft/signalr"

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Couleur Favorite';

  private hubConnection?: signalR.HubConnection;
  isConnected: boolean = false;

  nbFavoritesPerColor: number[] = [0, 0, 0, 0];

  recentMessages: string[] = [];
  text = "";

  selectedIndex = 0;

  favoriteColors: any[] = [
    {name: "Aucune", backgroundColor: "black"},
    {name: "Rouge", backgroundColor: "red"},
    {name: "Vert", backgroundColor: "green"},
    {name: "Bleu", backgroundColor: "cyan"}];

    connect() {
      this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl('http://localhost:5004/hubs/favoriteColor')
        .build();
    
      this.hubConnection
        .start()
        .then(() => {
          console.log('La connexion est live!');
          this.isConnected = true;
    
          if(this.hubConnection != undefined){
            this.hubConnection.on('InitFavorites', (data: number[]) => {
              this.nbFavoritesPerColor = data;
            });
      
            this.hubConnection.on('UpdateFavorites', (colorIndex: number, count: number) => {
              this.nbFavoritesPerColor[colorIndex] = count;
            });
      
            this.hubConnection.on('ReceiveMsg', (message: string) => {
              this.recentMessages.push(message);
            });
          }
        })
        .catch(err => console.log('Error while starting connection: ' + err))
    }
    

    disconnect() {
      if (this.hubConnection) {
        this.hubConnection.stop().then(() => {
          console.log('Disconnected');
          this.isConnected = false;
        }).catch(err => console.log('Error while disconnecting: ' + err));
      }
    }
    

    chooseColor(colorIndex:number) {
      this.selectedIndex = colorIndex;
      
      if (this.hubConnection) {
        this.hubConnection.invoke('ChooseColor', colorIndex)
          .catch(err => console.error(err));
      }
    
      this.recentMessages = [];
    }
    

    sendMessage() {
      if (this.hubConnection && this.text.trim() !== '') {
        this.hubConnection.invoke('SendMessage', this.text)
          .catch(err => console.error(err));
      }
    
      this.text = "";
    }
    
}
