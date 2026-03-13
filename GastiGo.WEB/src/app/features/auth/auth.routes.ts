import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { authRedirectGuard } from '@core/guards/auth-redirect.guard';

export const AUTH_ROUTE: Routes = [
  {
    path: 'signin',
    component: LoginComponent,
    canActivate: [authRedirectGuard]
  },

  {
    path: 'signup',
    component: RegisterComponent
  }

];
