import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Developer } from '../developer-list/developer.model';
import { DevelopersService } from '../developers.service';

@Component({
  selector: 'app-create-developer',
  templateUrl: './create-developer.component.html',
  styles: []
})
export class CreateDeveloperComponent implements OnInit {

  public developerForm: FormGroup;

  constructor(private fb: FormBuilder, private developerService:DevelopersService) { 

    this.developerForm = fb.group({
      name: [
        { value: null, disabled: false },
        [Validators.required]
      ],
      hackerName: [
        { value: null, disabled: false },
        [Validators.required]
      ],
      location: [
        { value: null, disabled: false },
        [Validators.required]
      ],
      bio: null,
    });

  }

  ngOnInit() {

  }

  public createDeveloper():void {
    let newDeveloper = <Developer>this.developerForm.value;
    this.developerService.createDeveloper(newDeveloper, () => {
      // Successfully created the developer, so we might want to do something else in here
      console.log("Created the developer");
      
    });
  }
}
