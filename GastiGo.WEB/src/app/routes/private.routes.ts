import { Routes } from '@angular/router';
import { AuthGuard } from '@core/guards/auth.guard';
import { PrivateLayoutComponent } from '@layouts/private-layout/private-layout.component';


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
        loadChildren: () => import('../features/dashboard/routes/dashboard.routes').then(m => m.DASHBOARD_ROUTE)
      },

      {
        path: 'categories',
        loadChildren: () => import('@features/finances/category/routes/category.routes').then(m => m.CATEGORIES_ROUTE)
      },

      {
        path: 'banks',
        loadChildren: () => import('@features/finances/bank/routes/bank.routes').then(m => m.BANK_ROUTE)
      },

      {
        path: 'account-types',
        loadChildren: () => import('@features/finances/type-account/routes/account-type.routes').then(m => m.ACCOUNT_TYPE_ROUTE)
      },

      {
        path: 'accounts',
        loadChildren: () => import('@features/finances/account/routes/account.routes').then(m => m.ACCOUNT_ROUTE)
      }
    ]
  }



];
