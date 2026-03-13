import { Injectable, signal } from '@angular/core';

/**
 * Servicio para gestionar el estado del layout, incluyendo el estado del sidebar y el menú móvil.
 */
@Injectable({
  providedIn: 'root'
})
export class LayoutService {

  private storageKey = 'gastigo-sidebar';
  collapsed = signal(false); // Estado del sidebar (colapsado o expandido)
  mobileMenuOpen = signal(false); // Estado del menú móvil (abierto o cerrado)

  // El constructor inicializa el estado del sidebar al cargar la aplicación
  constructor() {
    this.initSidebarState();
  }

  // Alterna entre el estado colapsado y expandido del sidebar
  toggleSidebar(): void {
    const next = !this.collapsed();
    this.collapsed.set(next);

    localStorage.setItem(
      this.storageKey,
      next ? 'collapsed' : 'expanded'
    );
  }

  // Alterna el estado del menú móvil (abierto/cerrado)
  toggleMobileMenu(): void {
    this.mobileMenuOpen.set(!this.mobileMenuOpen());
  }

  // Cierra el menú móvil
  closeMobileMenu(): void {
    this.mobileMenuOpen.set(false);
  }
  // Abre el menú móvil
  openMobileMenu(): void {
    this.mobileMenuOpen.set(true);
  }

  // Inicializa el estado del sidebar basado en la preferencia guardada en localStorage
  private initSidebarState(): void {
    const saved = localStorage.getItem(this.storageKey);

    if (saved === 'collapsed') {
      this.collapsed.set(true);
    }
  }

}
