import { Injectable, signal } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from '@core/models/common/menu-item.model';

/**
 * Servicio para gestionar el estado del menú, incluyendo los elementos del menú y el título de la página.
 */
@Injectable({
  providedIn: 'root'
})
export class MenuService {

  pageTitle = signal('Dashboard');
  menuItems: MenuItem[] = [
    { label: 'Dashboard',     icon: 'fa-solid fa-gauge', route: '/dashboard' },
    { label: 'Cuentas',       icon: 'fa-solid fa-file-zipper', route: '/accounts' },
    { label: 'Transacciones', icon: 'fa-solid fa-money-bill-transfer', route: '/transactions' },
    { label: 'Categorías',    icon: 'fa-solid fa-layer-group', route: '/categories' },
    { label: 'Bancos',        icon: 'fa-solid fa-university', route: '/banks' },
    { label: 'Configuración', icon: 'fa-solid fa-address-card', route: '/settings' }
  ];

  // El constructor inyecta el Router para navegar entre las rutas
  constructor(private router: Router) {}

  // Método para navegar a una ruta específica y actualizar el título de la página
  navigate(route: string): void {
    this.router.navigate([route]);
    this.updateTitle(route);
  }

  // Método privado para actualizar el título de la página basado en la ruta actual
  updateTitle(url: string): void {
    const path = url.toLowerCase(); // Convierte la URL a minúsculas para facilitar la comparación

    // Verifica la ruta y actualiza el título de la página en consecuencia
    if (path.includes('/transactions')) {
      this.pageTitle.set('Transacciones');
      return;
    }

    if (path.includes('/accounts')) {
      this.pageTitle.set('Cuentas');
      return;
    }

    if (path.includes('/categories')) {
      this.pageTitle.set('Categorías');
      return;
    }

    if (path.includes('/banks')) {
      this.pageTitle.set('Bancos');
      return;
    }

    if (path.includes('/settings')) {
      this.pageTitle.set('Configuración');
      return;
    }

    this.pageTitle.set('Dashboard');
  }

}
