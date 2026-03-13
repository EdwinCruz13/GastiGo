import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '@core/services/auth/auth.service';


/**
 * evita que un usuario autenticado pueda acceder a las rutas de login o registro
 * cuando el usuario esta autenticado, lo redirige al dashboard
 * @returns
 */
export const authRedirectGuard: CanActivateFn = () => {

  const auth = inject(AuthService);
  const router = inject(Router);

  if (auth.isAuthenticated()) {
    router.navigate(['/dashboard']);
    return false;
  }

  return true;
};
