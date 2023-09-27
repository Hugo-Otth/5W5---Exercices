import { Component } from '@angular/core';
import {transition, trigger, useAnimation} from "@angular/animations";
import {bounce, shake} from "ng-animate";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  animations:[
    trigger('bounce', [transition(':increment', useAnimation(bounce, {
      params: { timing: 3}
    }))]),
    trigger('shake', [transition(':decrement', useAnimation(shake, {
      params: { timing: 4}
    }))]),
  ]
})
export class AppComponent {
  mavariable = 0;
  shake= false;
  bounce = false;

  constructor() {
  }

  shakeMe() {
    this.shake = true;
    setTimeout(() => {this.shake = false;},3000);
  }

  bounceMe() {
    this.bounce = true;
    setTimeout(() => {this.bounce = false;},4000);
  }

  shakeBounceMe() {
    this.shake = true;
    console.log("shake on")
    setTimeout(() => { 
      console.log("shake off");
      this.shake = false; 
      this.bounce = true; 
      console.log("bounce on");
      setTimeout(() => {
        console.log("bounce off"); 
        this.bounce = false;
      },3000);
    },3000);
  }

  angularShakeBounce() {
    this.mavariable -=1;
    setTimeout(() => {
      this.mavariable += 1;
    }, 3000);
  }
}
