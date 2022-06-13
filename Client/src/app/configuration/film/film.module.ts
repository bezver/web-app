import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { FilmRoutingModule } from './film-routing.module';
import { FilmListComponent } from './film-list/film-list.component';
import { FilmDetailsComponent } from './film-details/film-details.component';
import { FilmCreateEditComponent } from './film-create-edit/film-create-edit.component';

import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';

import { DevExtremeModule, DxDataGridModule, DxButtonModule, DxLoadIndicatorModule } from 'devextreme-angular';


@NgModule({
  declarations: [
    FilmListComponent,
    FilmDetailsComponent,
    FilmCreateEditComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FilmRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxLoadIndicatorModule,
    DevExtremeModule,
    MatFormFieldModule,
    MatInputModule
  ]
})
export class FilmModule { }
