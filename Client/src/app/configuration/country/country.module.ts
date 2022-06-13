import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CountryRoutingModule } from './country-routing.module';
import { CountryListComponent } from './country-list/country-list.component';

import { DxDataGridModule, DxButtonModule, DxLoadIndicatorModule } from 'devextreme-angular';

@NgModule({
  declarations: [
    CountryListComponent
  ],
  imports: [
    CommonModule,
    CountryRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxLoadIndicatorModule
  ]
})
export class CountryModule { }
