import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ConfigurationRoutingModule } from './configuration-routing.module';
import { CountryModule } from './country/country.module';
import { GenreModule } from './genre/genre.module';



@NgModule({
  declarations: [

  ],
  imports: [
    CommonModule,
    ConfigurationRoutingModule,
    CountryModule,
    GenreModule
  ]
})
export class ConfigurationModule { }
