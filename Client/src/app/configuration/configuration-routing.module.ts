import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../core/auth/auth.guard';

const routes: Routes = [
  {
    path: 'film',
    loadChildren: () => import('./film/film.module').then(m => m.FilmModule),
    data: {
      role: 'System Admin'
    },
    canLoad: [AuthGuard]
  },
  {
    path: 'country',
    loadChildren: () => import('./country/country.module').then(m => m.CountryModule),
    data: {
      role: 'System Admin'
    },
    canLoad: [AuthGuard]
  },
  {
    path: 'genre',
    loadChildren: () => import('./genre/genre.module').then(m => m.GenreModule),
    data: {
      role: 'System Admin'
    },
    canLoad: [AuthGuard]
  },
  {
    path: '**',
    redirectTo: 'film'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ConfigurationRoutingModule { }
