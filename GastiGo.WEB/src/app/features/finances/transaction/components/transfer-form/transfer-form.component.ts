import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Input, OnInit, Output, signal } from '@angular/core';
import { ReactiveFormsModule, Validators, FormBuilder } from '@angular/forms';
import { Account } from '@core/models/finances/account.model';
import { Category } from '@core/models/finances/category.model';
import { TransactionRequestDTO } from '@core/models/finances/transaction.model';
import { TransactionType } from '@core/models/finances/transactionType.model';
import { AuthService } from '@core/services/auth/auth.service';
import { AccountService } from '@core/services/finances/account.service';

import { TransactionTypeService } from '@core/services/finances/transaction-type.service';
import { TransactionService } from '@core/services/finances/transaction.service';
import { DropdownSelectComponent } from '@shared/components/dropdown-select/dropdown-select.component';
import { ModalComponent } from '@shared/components/modal/modal.component';


@Component({
  selector: 'app-transfer-form',
  standalone: true,
  imports: [CommonModule, ModalComponent, DropdownSelectComponent, ReactiveFormsModule],
  templateUrl: './transfer-form.component.html'
})
export class TransferFormComponent implements OnInit {
  @Input() EntryType: 'TRANSFER' = 'TRANSFER';
  @Output() close = new EventEmitter<void>();
  @Output() result = new EventEmitter<boolean>();


  private cuentasServicio = inject(AccountService);
  private tipoTransaccionesServicio = inject(TransactionTypeService);
  private AuthServicio = inject(AuthService);
  private transaction = inject(TransactionService);

  //listados
  cuentas = signal<Account[] | []>([]);
  categorias = signal<Category[] | []>([]);
  tipoTransacciones = signal<TransactionType[] | []>([]);
  userID = signal<string>('');
  tipoTransactionSeleccionada = signal<TransactionType>({} as TransactionType);


  modalAlert = signal(false);
  modalMessageText = signal("");
  // Variable para almacenar los errores de la API y mostrarlos en la interfaz.
  apiErrors: string[] = [];


  //crear el formulario reactivo para agregar una nueva transacción
  formBuilder = inject(FormBuilder);
  transactionForm = this.formBuilder.group({
    userId: [this.userID()],
    transactionTypeId: [this.tipoTransactionSeleccionada().transactionTypeId ?? null],
    categoryId: [null as string | null],
    description: ['', [Validators.required, Validators.nullValidator, Validators.maxLength(120)]],
    fromAccountId: [null as string | null],
    toAccountId: [null as string | null],
    amount: [0, [Validators.required, Validators.nullValidator, Validators.min(0.01)]],
    dateTransaction: [new Date(), [Validators.required, Validators.nullValidator]],
    entryType: [this.EntryType]
  });


  //#region cargar datos

  ngOnInit() {
    this.cargarUsuario();
    this.cargarCuentas();
    this.cargarTiposTransacciones();


  }

  //metodo para cargar al usuario logueado
  cargarUsuario() {
    this.userID.set(this.AuthServicio.userId() ?? '');
  }

  //metodo para cargar las cuentas existentes
  cargarCuentas() {
    this.cuentasServicio.getAccounts(this.userID()).subscribe(response => {
      if (response.success) {
        this.cuentas.set(response.data ?? []);
      } else {
        // Manejar el error, por ejemplo, mostrando un mensaje al usuario
        this.modalMessageText.set('No se encontraron cuentas para este usuario.');
        this.modalAlert.set(true);
      }
    });
  }


  //metodo para cargar los tipos de transacciones existentes
  cargarTiposTransacciones() {
    this.tipoTransaccionesServicio.getTransactionTypes().subscribe(response => {
      if (response.success) {
        this.tipoTransacciones.set(response.data ?? []);
        this.tipoTransactionSeleccionada.set(this.tipoTransacciones().filter(tipo => tipo.code === 'TRF')[0]);
      } else {
        // Manejar el error, por ejemplo, mostrando un mensaje al usuario
        this.modalMessageText.set('No se encontraron tipos de transacciones.');
        this.modalAlert.set(true);
      }
    });
  }

  //#endregion

  //#region eventos de dropdown

  //evento para cuando se selecciona una cuenta en el dropdown de cuentas
  onAccountChangeOrigin(accountId: string) {
    this.transactionForm.patchValue({
      fromAccountId: accountId ?? null,
      transactionTypeId: this.tipoTransactionSeleccionada().transactionTypeId ?? null,
      userId: this.AuthServicio.userId(),
      dateTransaction: new Date()
    });
  }

  onAccountChangeDestination(accountId: string) {
    this.transactionForm.patchValue({
      toAccountId: accountId ?? null,
      transactionTypeId: this.tipoTransactionSeleccionada().transactionTypeId ?? null,
      userId: this.AuthServicio.userId(),
      dateTransaction: new Date()
    });
  }



  //#endregion

  //#region acciones



  onSubmit() {
    if (this.transactionForm.valid) {
      const formValue = this.transactionForm.value as TransactionRequestDTO;
      console.log('Formulario válido, valores:', formValue);
      this.saveTransaction(formValue);
    } else {

      console.log('Formulario no válido');
    }
  }

  //metodo para guardar
  saveTransaction(formValue: TransactionRequestDTO) {
    this.transaction.createTransaction(formValue).subscribe({
      next: (response) => {
        if (response.success) {
          // Cerrar el modal y mostrar un mensaje de éxito
          this.ngOnInit(); // Recargar las cuentas para mostrar la nueva cuenta en la lista

          this.modalMessageText.set('Transacción creada exitosamente.');
          this.modalAlert.set(true);
          this.transactionForm.reset();


          this.result.emit(true); // Emitir el evento para cerrar el formulario

        } else {
          this.modalMessageText.set(response.message || 'Error al crear la transacción.');
          this.modalAlert.set(true);
        }
      },
      error: (error) => {
        this.modalMessageText.set('Error al crear la transacción.');
        this.modalAlert.set(true);
      }
    });
  }



  //#endregion

}
