import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeveloperListComponent } from './developer-list/developer-list.component';
import { DevelopersService } from './developers.service';
import { CreateDeveloperComponent } from './create-developer/create-developer.component';
import { MatTableModule, MatFormFieldModule, MatInputModule, MatSnackBarModule, MatButtonModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    MatTableModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSnackBarModule,
    MatButtonModule
  ],
  declarations: [DeveloperListComponent, CreateDeveloperComponent],
  providers: [DevelopersService],
  exports: [DeveloperListComponent, CreateDeveloperComponent]
})
export class DevelopersModule { }
