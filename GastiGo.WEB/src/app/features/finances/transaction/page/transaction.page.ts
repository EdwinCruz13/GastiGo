import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TransactionTypeService } from '@core/services/finances/transaction-type.service';
import { TransactionService } from '@core/services/finances/transaction.service';
import { TransactionType } from '@core/models/finances/transactionType.model';
import { BalanceDTO } from '@core/models/finances/transaction.model';
import { ModalComponent } from '@shared/components/modal/modal.component';
import { AuthService } from '@core/services/auth/auth.service';
import { EntryFormComponent } from '../components/entry-form/entry-form.component';
import { TransferFormComponent } from '../components/transfer-form/transfer-form.component';
import { AccountTypeService } from '@core/services/finances/account-type.service';
import { DropdownSelectComponent } from '@shared/components/dropdown-select/dropdown-select.component';
import { CardComponent } from '@shared/components/card/card.component';
import { AccountService } from '@core/services/finances/account.service';
import { Account } from '@core/models/finances/account.model';
import { DateInputComponent } from '@shared/components/input-date/date-input.component';
import { TableComponent } from '@shared/components/table/table.component';



@Component({
  selector: 'app-transaction.page',
  standalone: true,
  imports: [CommonModule, ModalComponent, DropdownSelectComponent, CardComponent, DateInputComponent, TableComponent, EntryFormComponent, TransferFormComponent],
  templateUrl: './transaction.page.html'
})
export class TransactionPage implements OnInit {
  private transaccionesServicio = inject(TransactionService);
  private tiposTransaccionesServicio = inject(TransactionTypeService);
  private AuthServicio = inject(AuthService);
  private tipoCuentaServicio = inject(AccountTypeService);
  private cuentaServicio = inject(AccountService);

  //listados
  transactiones = signal<BalanceDTO[] | []>([]);
  tiposTransacciones = signal<TransactionType[] | []>([]);

  cuentas = signal<Account[] | []>([]);
  cuentaSeleccionada = signal<Account | null>(null);
  selectedAccountId = computed(() => this.cuentaSeleccionada()?.accountId ?? null);


  // variable para obtener el usuario logueado
  userID = signal<string>('');

  showEntryForm = signal(false);
  showTransferForm = signal(false);
  tipoMov = signal<'IN' | 'OUT' | 'TRANSFER'>('IN');



  modalAlert = signal(false);
  modalMessageText = signal("");


  date1 = signal<string>("");
  date2 = signal<string>("");

  //#region cargar datos

  ngOnInit() {
    this.cargarUsuario();
    this.CargarCuentas();
    this.cargarTiposTransacciones();
    this.setFechasMesActual();
  }


  cargarUsuario() {
    this.userID.set(this.AuthServicio.userId() ?? '');
  }

  //metodo para cargar los tipos de transacciones existentes
  cargarTiposTransacciones() {
    this.tiposTransaccionesServicio.getTransactionTypes().subscribe(response => {
      if (response.success) {
        this.tiposTransacciones.set(response.data ?? []);
      } else {
        this.modalMessageText.set('Error al cargar los tipos de transacciones.');
        this.modalAlert.set(true);
      }

    });
  }


  CargarCuentas() {
    this.cuentaServicio.getAccounts(this.userID()).subscribe({
      next: (response) => {
        if (response.success) {
          this.cuentas.set(response.data ?? []);

          //seleccionar la primera cuenta por defecto
          if (response.data && response.data.length > 0) {
            this.cuentaSeleccionada.set(response.data[0]);
            this.selectedAccountId = computed(() => this.cuentaSeleccionada()?.accountId ?? null);
            console.log('Cuenta seleccionada por defecto:', this.selectedAccountId());

          }

        } else {
          this.modalMessageText.set(response.message || 'Error al cargar las cuentas.');
          this.modalAlert.set(true);
        }
      },
      error: (error) => {
        this.modalMessageText.set('Error al cargar las cuentas.');
        this.modalAlert.set(true);
      }
    });
  }


    //metodo para cargar las transacciones existentes
  cargarTransacciones() {
    this.transaccionesServicio.getTransactionsByDate(this.userID(), this.selectedAccountId(), this.date1(), this.date2()).subscribe(response => {
      if (response.success) {
        this.transactiones.set(response.data ?? []);
      } else {
        this.modalMessageText.set('No se encontraron transacciones para este usuario.');
        this.modalAlert.set(true);
      }
    });
  }

  //#endregion


  //#region acciones

  //#region acciones para agregar una nueva transacción

  addTransaction(type: 'IN' | 'OUT' | 'TRANSFER' = 'IN') {
    this.tipoMov.set(type);
    //si el tipo de transacción es transferencia, abrir el formulario para agregar una nueva transferencia
    if (type === 'TRANSFER') {
      this.showTransferForm.set(true);
    }
    else {
      //abrir el formulario para agregar una nueva transacción de ingreso o gasto
      this.showEntryForm.set(true);
    }

  }

  //metodo para agregar una nueva transacción
  //abre el modal con el formulario para agregar una nueva transacción
  closeEntryForm() {
    this.showEntryForm.set(false);
  }

  closeEntryFormByResult(result: boolean) {
    this.showEntryForm.set(false);
    this.ngOnInit(); // Recargar las transacciones para
  }

  //#endregion


  //metodo para agregar una nueva transferencia
  onAccountChanged(accountId: string) {
    //buscar la primera cuenta que tenga el tipo de cuenta seleccionado
    const cuenta = this.cuentas().find(c => c.accountId === accountId);

    if (cuenta) {
      this.cuentaSeleccionada.set(cuenta);

      //buscar las transacciones segun cuenta y Fechas
      this.cargarTransacciones();
    }
  }

  //metodos para manejar el cambio de fechas
  onDate1Changed(value: string) {
    this.date1.set(value);
    this.cargarTransacciones();
  }

  //metodos para manejar el cambio de fechas
  onDate2Changed(value: string) {
    this.date2.set(value);
    this.cargarTransacciones();
  }


  //#endregion




  //metodo para establecer las fechas del mes actual
  setFechasMesActual() {
    const hoy = new Date();

    const primerDia = new Date(hoy.getFullYear(), hoy.getMonth(), 1);
    const ultimoDia = new Date(hoy.getFullYear(), hoy.getMonth() + 1, 0);

    this.date1.set(this.formatDate(primerDia));
    this.date2.set(this.formatDate(ultimoDia));
  }

  // Formatea la fecha a yyyy-MM-dd
  private formatDate(date: Date): string {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');

    return `${year}-${month}-${day}`; // yyyy-MM-dd
  }
}
