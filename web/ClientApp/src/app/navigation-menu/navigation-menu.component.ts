import { Component, OnInit, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-navigation-menu',
  templateUrl: './navigation-menu.component.html',
  styles: []
})
export class NavigationMenuComponent implements OnInit {

  @Output() onCloseMenu:EventEmitter<any> = new EventEmitter<any>();
  constructor() { }

  ngOnInit() {
  }

  public closeMenu():void {
    this.onCloseMenu.emit();
  }

}
