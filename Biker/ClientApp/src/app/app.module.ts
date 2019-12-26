import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ToastrModule } from 'ngx-toastr';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app/app.component';
import { NavMenuComponent } from './navmenu/navmenu.component';
import { HomeComponent } from './home/home.component';
import { BikeFormComponent } from './bike-form/bike-form.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppErrorHandler } from './app.error-handler';
import { BikeListComponent } from './bike-list/bike-list.component';
import { PaginationComponent } from './shared/pagination.component';

import * as Sentry from "@sentry/browser";
import { ViewBikeComponent } from './view-bike/view-bike.component';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { LoginComponent } from './user/login/login.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { AuthInterceptor } from './auth/auth.interceptor';
import { UserService } from './services/user.service';

Sentry.init({
  dsn: "https://9186aac887414930a5469ccd467b1217@sentry.io/1833261"
});

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    BikeFormComponent,
    BikeListComponent,
    PaginationComponent,
    ViewBikeComponent,
    UserComponent,
    RegistrationComponent,
    LoginComponent,
    UserProfileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      preventDuplicates: true,
      timeOut: 2000,
      closeButton: true,
      progressBar: true,
      progressAnimation: 'increasing'
    })
  ],
  providers: [
    { provide: ErrorHandler, useClass: AppErrorHandler },
    UserService, {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
