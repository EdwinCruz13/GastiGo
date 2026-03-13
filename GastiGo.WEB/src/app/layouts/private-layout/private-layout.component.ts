import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';


import { ThemeService } from '@core/services/common/theme/theme.service';
import { LayoutService } from '@core/services/common/layout/layout.service';
import { MenuService } from '@core/services/common/menu/menu.service';
import { AuthService } from '@core/services/auth/auth.service';

import { SidebarComponent } from './sidebar/sidebar.component';
import { TopbarComponent } from './topbar/topbar.component';


@Component({
  selector: 'app-private-layout',
  standalone: true,
  imports: [CommonModule, RouterOutlet, TopbarComponent, SidebarComponent],
  templateUrl: './private-layout.component.html',
  styleUrls: ['./private-layout.component.css']
})
/**
 * PrivateLayoutComponent es el componente principal para la interfaz de usuario autenticada.
 * Este componente maneja la estructura general de la aplicación, incluyendo la barra lateral, el encabezado y el contenido principal.
 * Utiliza servicios para manejar el tema, el diseño y el menú de navegación.
 */
export class PrivateLayoutComponent {
  theme = inject(ThemeService); // inyecta el servicio de tema
  layout = inject(LayoutService); // inyecta el servicio de layout

}
