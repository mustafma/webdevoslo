import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public menuOpened:boolean;
  constructor() {
    this.menuOpened = false;
  }
  public onToggleMenu():void {
    this.menuOpened = !this.menuOpened;
  }
  public closeMenu():void {
    this.menuOpened = false;
  }
}
