import { Routes } from '@angular/router';
import { LoginPage } from '@features/auth/pages/login/login.page'
import { RegisterPage } from '@features/auth/pages/register/register.page'
import { authRedirectGuard } from '@core/guards/auth-redirect.guard';

export const AUTH_ROUTE: Routes = [
  {
    path: 'signin',
    component: LoginPage,
    canActivate: [authRedirectGuard]
  },

  {
    path: 'signup',
    component: RegisterPage
  }

];
