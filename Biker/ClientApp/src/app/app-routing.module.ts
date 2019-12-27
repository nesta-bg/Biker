import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { BikeFormComponent } from './bike-form/bike-form.component';
import { BikeListComponent } from './bike-list/bike-list.component';
import { ViewBikeComponent } from './view-bike/view-bike.component';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { LoginComponent } from './user/login/login.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { AuthGuard } from './auth/auth.guard';
import { AdminComponent } from './admin/admin.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';

const routes: Routes = [
  { path: '', redirectTo: 'bikes', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'bikes/new', component: BikeFormComponent },
  { path: 'bikes/edit/:id', component: BikeFormComponent },
  { path: 'bikes/:id', component: ViewBikeComponent },
  { path: 'bikes', component: BikeListComponent },
  { path: 'user', component: UserComponent, children: [
    {  path: 'registration', component: RegistrationComponent  },
    {  path: 'login', component: LoginComponent  },]  },
  { path:'user-profile', component: UserProfileComponent, canActivate:[AuthGuard] },
  { path: 'admin', component: AdminComponent },
  { path: 'forbidden', component: ForbiddenComponent },
  { path: '**', redirectTo: 'home'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
