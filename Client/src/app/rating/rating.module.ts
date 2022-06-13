import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FilmListComponent } from './film-list/film-list.component';
import { RatingRoutingModule } from './rating-routing.module';

import { DxDataGridModule, DxButtonModule, DxLoadIndicatorModule } from 'devextreme-angular';

@NgModule({
  declarations: [
    FilmListComponent
  ],
  imports: [
    CommonModule,
    RatingRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxLoadIndicatorModule
  ]
})
export class RatingModule { }
