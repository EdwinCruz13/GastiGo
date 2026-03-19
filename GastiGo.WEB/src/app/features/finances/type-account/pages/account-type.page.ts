import { Component, inject, signal, OnInit } from '@angular/core';

import { CommonModule } from '@angular/common';
import { AccountTypeService } from '@core/services/finances/account-type.service';

import { AccountType } from '@core/models/finances/accountType.model';
import { TableComponent } from '@shared/components/table/table.component';
import { ModalComponent } from '@shared/components/modal/modal.component';


@Component({
  selector: 'app-account-type.page',
  imports: [CommonModule, TableComponent, ModalComponent],
  templateUrl: './account-type.page.html',
  styleUrl: './account-type.page.css',
})
export class AccountTypePage implements OnInit {
  //#region Declaracion de variables
  private accountTypeService = inject(AccountTypeService);
  listadoTiposCuentas = signal<AccountType[]>([]);

    // Variable para mostrar un mensaje de éxito después de guardar una categoría.
   modalAlert = signal(false);
   modalMessageText = signal("");

  //#endregion

  //#region eventos loads
  ngOnInit(): void {
    this.CargarTipoCuentas();
  }
  //#endregion

  //Metodo para cargar los tipos de cuentas
  private CargarTipoCuentas(): void {
    this.accountTypeService.getAccountTypes().subscribe({
      next: (response) => {
        if (response.success) {
          // Manejar la respuesta exitosa
          this.listadoTiposCuentas.set(response.data ?? []);
        } else {
          // Manejar el error
          this.modalMessageText.set("No se pudieron cargar los tipos de cuentas. Intente nuevamente más tarde.");
          this.modalAlert.set(true);
        }
      },
      error: (err) => {
        // Manejar el error de la solicitud HTTP
        this.modalMessageText.set("Ocurrió un error al cargar los tipos de cuentas. Intente nuevamente más tarde.");
        this.modalAlert.set(true);
      }
    });
  }

}
