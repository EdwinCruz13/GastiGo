import { Routes } from '@angular/router';

import { TransactionPage } from '../page/transaction.page';

/**
 * Rutas para la gestión de tipos de cuentas.
 * Esta ruta se carga de forma perezosa (lazy loading)
 * cuando el usuario navega a la sección de tipos de cuentas
 * en la aplicación.
 */
export const TRANSACTION_ROUTE: Routes = [
  {
    path: '',
    component: TransactionPage
  },
];
