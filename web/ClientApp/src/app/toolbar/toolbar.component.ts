import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styles: []
})
export class ToolbarComponent implements OnInit {

  @Output() menutoggle:EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(private router: Router) { }

  ngOnInit() {
  
  }

  public toggleMenu():void {
    this.menutoggle.emit(true);
  }

  public createDeveloper():void {
    this.router.navigate(["/create-developer"]);
  }
}
