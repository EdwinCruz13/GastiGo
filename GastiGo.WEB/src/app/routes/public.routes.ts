import { Routes } from '@angular/router';
import { AUTH_ROUTE } from '../features/auth/auth.routes';


/**
 * adjunta la ruts publicas, por ahora solo existe las rutas de auth
 *
 */
export const PUBLIC_ROUTES: Routes = [
  {
    path: 'auth',
    loadChildren: () => import('../features/auth/auth.routes').then(m => m.AUTH_ROUTE)
  }
];
