import { LayoutService } from '@core/services/common/layout/layout.service';
import { Component, inject, OnInit } from '@angular/core';
import { MenuService } from '@core/services/common/menu/menu.service';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
  imports: [RouterLink, RouterLinkActive]
})
export class SidebarComponent {
  layout = inject(LayoutService); // inyecta el servicio de layout
  menu = inject(MenuService); // inyecta el servicio de menú

}
