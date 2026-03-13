import { Routes } from '@angular/router';
import { PUBLIC_ROUTES } from './routes/public.routes';
import { PRIVATE_ROUTES } from './routes/private.routes';

export const APPROUTES: Routes = [

  //redireccion principal
  {
    path: '',
    redirectTo: 'auth/signin',
    pathMatch: 'full'
  },

  ...PUBLIC_ROUTES,
  ...PRIVATE_ROUTES,

  //rutas no encontradas
  {
    path: '**',
    redirectTo: 'auth/signin'
  }

];
