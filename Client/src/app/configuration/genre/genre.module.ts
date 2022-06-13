import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GenreRoutingModule } from './genre-routing.module';
import { GenreListComponent } from './genre-list/genre-list.component';

import { DxDataGridModule, DxButtonModule, DxLoadIndicatorModule } from 'devextreme-angular';

@NgModule({
  declarations: [
    GenreListComponent
  ],
  imports: [
    CommonModule,
    GenreRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxLoadIndicatorModule
  ]
})
export class GenreModule { }
