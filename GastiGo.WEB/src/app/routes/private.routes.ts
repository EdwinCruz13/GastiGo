import { Routes } from '@angular/router';
import { AuthGuard } from '@core/guards/auth.guard';
import { PrivateLayoutComponent } from '../layouts/private-layout/private-layout.component';


/**
 * Adjunta la rutas privadas
 */
export const PRIVATE_ROUTES: Routes = [
   {
    path: '',
    component: PrivateLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'dashboard',
        loadChildren: () => import('../features/dashboard/dashboard.routes').then(m => m.DASHBOARD_ROUTE)
      },
      // {
      //   path: 'accounts',
      //   loadChildren: () => import('../features/accounts/accounts.routes').then(m => m.ACCOUNTS_ROUTE)
      // },
      // {
      //   path: 'transactions',
      //   loadChildren: () => import('../features/transactions/transactions.routes').then(m => m.TRANSACTIONS_ROUTE)
      // },
      // {
      //   path: 'categories',
      //   loadChildren: () => import('../features/categories/categories.routes').then(m => m.CATEGORIES_ROUTE)
      // },
      // {
      //   path: 'settings',
      //   loadChildren: () => import('../features/settings/settings.routes').then(m => m.SETTINGS_ROUTE)
      // }
    ]
  }



];
