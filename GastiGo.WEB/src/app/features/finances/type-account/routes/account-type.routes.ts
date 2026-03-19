import { Routes } from '@angular/router';

import { AccountTypePage } from './../pages/account-type.page';

/**
 * Rutas para la gestión de tipos de cuentas.
 * Esta ruta se carga de forma perezosa (lazy loading)
 * cuando el usuario navega a la sección de tipos de cuentas
 * en la aplicación.
 */
export const ACCOUNT_TYPE_ROUTE: Routes = [
  {
    path: '',
    component: AccountTypePage
  },
];
