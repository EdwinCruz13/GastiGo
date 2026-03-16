import { Routes } from '@angular/router';
import { authRedirectGuard } from '@core/guards/auth-redirect.guard';
import { CategoryPage } from '../pages/category.page';

export const CATEGORIES_ROUTE: Routes = [
  {
    path: '',
    component: CategoryPage,
  },

  

];
