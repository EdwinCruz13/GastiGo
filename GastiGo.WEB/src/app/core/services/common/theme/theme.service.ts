import { Injectable, signal } from '@angular/core';

/**
 * Servicio para gestionar el tema de la aplicación (claro/oscuro).
 */
@Injectable({
  providedIn: 'root'
})
export class ThemeService {

  private storageKey = 'gastigo-theme';
  darkMode = signal(false);

  // El constructor inicializa el tema al cargar la aplicación
  constructor() {
    this.initTheme();
  }

  // Alterna entre el tema claro y oscuro
  toggleTheme(): void {
    const next = !this.darkMode();
    this.setTheme(next);
  }

  // Establece el tema explícitamente
  setTheme(isDark: boolean): void {
    this.darkMode.set(isDark);
    document.body.classList.toggle('dark-mode', isDark);
    localStorage.setItem(
      this.storageKey,
      isDark ? 'dark' : 'light'
    );
  }

  // Inicializa el tema basado en la preferencia guardada en localStorage
  private initTheme(): void {
    const saved = localStorage.getItem(this.storageKey);
    const isDark = saved === 'dark';
    this.darkMode.set(isDark);
    document.body.classList.toggle('dark-mode', isDark);
  }
}
