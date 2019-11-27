import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { BikeFormComponent } from './bike-form/bike-form.component';
import { BikeListComponent } from './bike-list/bike-list.component';

const routes: Routes = [
  { path: '', redirectTo: 'bikes', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'bikes/new', component: BikeFormComponent },
  { path: 'bikes/:id', component: BikeFormComponent },
  { path: 'bikes', component: BikeListComponent },
  { path: '**', redirectTo: 'home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
