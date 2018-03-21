import { Component, OnInit } from '@angular/core';
import { DevelopersService } from '../developers.service';
import { Developer } from './developer.model';

@Component({
  selector: 'app-developer-list',
  templateUrl: './developer-list.component.html',
  styles: []
})
export class DeveloperListComponent implements OnInit {

  public developerList: Developer[] = [];
  public displayedColumns = ['name', 'hackerName', 'location', 'bio'];
  
  constructor(private developersService:DevelopersService) { }

  ngOnInit() {
    this.developersService.getDevelopers().subscribe(response => {
      //console.log(response);
      this.developerList = response;
    });
  }
}
