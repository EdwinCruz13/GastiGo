import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Input, OnInit, Output, signal } from '@angular/core';
import { ReactiveFormsModule, Validators, FormBuilder } from '@angular/forms';
import { Account } from '@core/models/finances/account.model';
import { Category } from '@core/models/finances/category.model';
import { TransactionType } from '@core/models/finances/transactionType.model';
import { AuthService } from '@core/services/auth/auth.service';
import { AccountService } from '@core/services/finances/account.service';
import { CategoryService } from '@core/services/finances/category.service';
import { TransactionTypeService } from '@core/services/finances/transaction-type.service';
import { DropdownSelectComponent } from '@shared/components/dropdown-select/dropdown-select.component';
import { ModalComponent } from '@shared/components/modal/modal.component';


@Component({
  selector: 'app-transfer-form',
  standalone: true,
  imports: [CommonModule, ModalComponent, DropdownSelectComponent ,ReactiveFormsModule],
  templateUrl: './transfer-form.component.html'
})
export class TransferFormComponent implements OnInit {
  @Input() EntryType: 'TRANSFER' = 'TRANSFER';
  @Output() close = new EventEmitter<void>();


  private cuentasServicio = inject(AccountService);
  private tipoTransaccionesServicio = inject(TransactionTypeService);
  private AuthServicio = inject(AuthService);

  //listados
  cuentas = signal<Account[] | []>([]);
  categorias = signal<Category[] | []>([]);
  tipoTransacciones = signal<TransactionType[] | []>([]);
  userID = signal<string>('');


  modalAlert = signal(false);
  modalMessageText = signal("");
  // Variable para almacenar los errores de la API y mostrarlos en la interfaz.
  apiErrors: string[] = [];


  //crear el formulario reactivo para agregar una nueva transacción
  formBuilder = inject(FormBuilder);
  transactionForm = this.formBuilder.group({
    userId: [this.userID()],
    cuentaOrigenId: [null as string | null],
    cuentaDestinoId: [null as string | null],
    description: ['', [Validators.required, Validators.nullValidator, Validators.maxLength(120)]],
    amount: [0, [Validators.required, Validators.nullValidator, Validators.min(0.01)]],
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

    }

    onAccountChangeDestination(accountId: string) {

    }



    //#endregion

    //#region acciones



    onSubmit() {
    }
    //#endregion

}
