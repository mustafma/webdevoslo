import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Developer } from './developer-list/developer.model';
import { MatSnackBar } from '@angular/material';

@Injectable()
export class DevelopersService {

  constructor(private httpClient: HttpClient, private snackbar: MatSnackBar) { }

  public getDevelopers(): Observable<Developer[]> {
    return this.httpClient.get<Developer[]>('api/Developer');

  }

  public createDeveloper(developer:Developer, successCallback:Function):void {
    this.httpClient.post('api/Developer/Add',developer)
      .subscribe(
        Response => {
          this.snackbar.open("A developer was created","OK", { duration: 3000});
          successCallback();
        },
        Error =>  {
          this.snackbar.open("There as an error creating the developer!", "OK", { duration: 3000});
          console.log("Error saving developer:"+Error);
        }
      )
  }
}
