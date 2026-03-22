import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TransactionTypeService } from '@core/services/finances/transaction-type.service';
import { TransactionService } from '@core/services/finances/transaction.service';
import { DataListComponent } from '@shared/components/datalist/datalist.component';
import { TransactionType } from '@core/models/finances/transactionType.model';
import { Transaction } from '@core/models/finances/transaction.model';
import { ModalComponent } from '@shared/components/modal/modal.component';
import { AuthService } from '@core/services/auth/auth.service';



@Component({
  selector: 'app-transaction.page',
  imports: [CommonModule, ModalComponent ,DataListComponent],
  templateUrl: './transaction.page.html'
})
export class TransactionPage implements OnInit { 
  private transaccionesServicio = inject(TransactionService);
  private tiposTransaccionesServicio = inject(TransactionTypeService);
  private AuthServicio = inject(AuthService);

  //listados
  transactiones = signal<Transaction[] | []>([]);
  tiposTransacciones = signal<TransactionType[] | []>([]);

  // variable para obtener el usuario logueado
  userID = signal<string>('');


  modalAlert = signal(false);
  modalMessageText = signal("");

  //#region cargar datos

  ngOnInit() {
    this.cargarUsuario();
    this.cargarTransacciones();
    this.cargarTiposTransacciones();
  }

  cargarUsuario() {
    this.userID.set(this.AuthServicio.userId() ?? '');
  }

  //metodo para cargar las transacciones existentes
  cargarTransacciones() {
    this.transaccionesServicio.getTransactions(this.userID()).subscribe(response => {
      if (response.success) {
        this.transactiones.set(response.data ?? []);
      } else {
        this.modalMessageText.set('Error al cargar las transacciones.');
        this.modalAlert.set(true);
      }
    });
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

  //#endregion


  //#region acciones

  //metodo para agregar una nueva transacción
  //abre el modal con el formulario para agregar una nueva transacción
  addTransaction() {
    // Lógica para agregar una nueva transacción
    this.modalMessageText.set('Funcionalidad para agregar una nueva transacción aún no implementada.');  
  }

  viewTransaction(transaction: Transaction){

  }

  editTransaction(transaction: Transaction){

  }

  deleteTransaction(transaction: Transaction){

  }
  //#endregion
}
