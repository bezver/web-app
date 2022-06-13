import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/core/auth/auth.guard';
import { FilmCreateEditComponent } from './film-create-edit/film-create-edit.component';
import { FilmDetailsComponent } from './film-details/film-details.component';
import { FilmListComponent } from './film-list/film-list.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: FilmListComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'create',
    component: FilmCreateEditComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'details/:id',
    component: FilmDetailsComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'details/:id/edit',
    component: FilmCreateEditComponent,
    canActivate: [AuthGuard]
  },
  {
    path: '**',
    redirectTo: ''
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FilmRoutingModule { }
