import { Component, HostListener, inject, OnInit, signal } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs/operators';


import { AuthService } from '@core/services/auth/auth.service';
import { LayoutService } from '@core/services/common/layout/layout.service';
import { MenuService } from '@core/services/common/menu/menu.service';
import { ThemeService } from '@core/services/common/theme/theme.service';

@Component({
  selector: 'app-topbar',
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.css']
})
export class TopbarComponent {
  theme = inject(ThemeService); // inyecta el servicio de tema
  auth = inject(AuthService); // inyecta el servicio de autenticación
  layout = inject(LayoutService); // inyecta el servicio de layout
  menu = inject(MenuService); // inyecta el servicio de menú

  userMenuOpen = signal(false);


  // El constructor se suscribe a los eventos de navegación para actualizar el título de la página y cerrar el menú móvil
  constructor(private router: Router) {
      this.router.events
        .pipe(filter(event => event instanceof NavigationEnd))
        .subscribe(() => {
          this.menu.updateTitle(this.router.url);
          this.layout.closeMobileMenu();
          this.userMenuOpen.set(false);
        });

      this.menu.updateTitle(this.router.url);
  }

  toggleUserMenu(): void {
    this.userMenuOpen.set(!this.userMenuOpen());
  }

  logout(): void {
    this.auth.logout();
    location.href = "/auth/signin";
  }

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent) {

    const target = event.target as HTMLElement;

    if (!target.closest('.user-menu-wrapper')) {
      this.userMenuOpen.set(false);
    }

  }

}
