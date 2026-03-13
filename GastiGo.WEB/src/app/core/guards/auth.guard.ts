import { inject } from '@angular/core';
import { Router, type CanActivateFn } from '@angular/router';

/**
 * proteje la rutas,
 * si existe el token, esta autenticado
 * @param route
 * @param state
 * @returns
 */
export const AuthGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);

  const token = localStorage.getItem("token");

  if(token){
    return true;
  }

  router.navigate(['/auth/login']);
  return false;
};
