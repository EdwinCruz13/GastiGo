import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Input, OnInit, Output, signal } from '@angular/core';
import { ReactiveFormsModule, Validators, FormBuilder } from '@angular/forms';
import { Account } from '@core/models/finances/account.model';
import { Category } from '@core/models/finances/category.model';
import { TransactionRequestDTO } from '@core/models/finances/transaction.model';
import { TransactionType } from '@core/models/finances/transactionType.model';
import { AuthService } from '@core/services/auth/auth.service';
import { AccountService } from '@core/services/finances/account.service';
import { CategoryService } from '@core/services/finances/category.service';
import { TransactionTypeService } from '@core/services/finances/transaction-type.service';
import { TransactionService } from '@core/services/finances/transaction.service';
import { DropdownSelectComponent } from '@shared/components/dropdown-select/dropdown-select.component';
import { ModalComponent } from '@shared/components/modal/modal.component';

@Component({
  selector: 'app-entry-form',
  standalone: true,
  imports: [CommonModule, ModalComponent, DropdownSelectComponent ,ReactiveFormsModule],
  templateUrl: './entry-form.component.html'
})
export class EntryFormComponent implements OnInit {
  @Input() EntryType: 'IN' | 'OUT' | 'TRANSFER' = 'IN';
  @Output() close = new EventEmitter<void>();
  @Output() result = new EventEmitter<boolean>();

  private cuentasServicio = inject(AccountService);
  private categoriasServicio = inject(CategoryService);
  private tipoTransaccionesServicio = inject(TransactionTypeService);
  private AuthServicio = inject(AuthService);
  private transaction = inject(TransactionService);

  //listados
  cuentas = signal<Account[] | []>([]);
  categorias = signal<Category[] | []>([]);
  tipoTransacciones = signal<TransactionType[] | []>([]);
  tipoTransactionSeleccionada = signal<TransactionType>({} as TransactionType);
  userID = signal<string>('');

  tipoMov = signal<string>('');
  modalAlert = signal(false);
  modalMessageText = signal("");
  // Variable para almacenar los errores de la API y mostrarlos en la interfaz.
  apiErrors: string[] = [];



  //crear el formulario reactivo para agregar una nueva transacción
  formBuilder = inject(FormBuilder);
  transactionForm = this.formBuilder.group({
    userId: [this.AuthServicio.userId()],
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
    this.cargarCategorias();
    this.cargarTiposTransacciones();

    // Establecer el tipo de movimiento para mostrar en el formulario dependiendo del tipo de transacción que se va a agregar
    if(this.EntryType === 'IN'){
      this.tipoMov.set('Cuenta Destino');
    } else if(this.EntryType === 'OUT'){
      this.tipoMov.set('Cuenta de Origen');
    }

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

    //metodo para cargar las categorias existentes
    cargarCategorias() {
      this.categoriasServicio.getCategories(this.userID()).subscribe(response => {
        if (response.success) {
          this.categorias.set(response.data ?? []);
          if(this.EntryType === 'OUT'){
            //filtrar categorias para mostrar solo las de naturaleza de gasto
            this.categorias.set(this.categorias().filter(categoria => categoria.nature.abbre === 'E' && categoria.parentId != null && categoria.isActive));
          }

          if(this.EntryType === 'IN'){
            //filtrar categorias para mostrar solo las de naturaleza de ingreso
            this.categorias.set(this.categorias().filter(categoria => categoria.nature.abbre === 'I' && categoria.parentId != null && categoria.isActive));

          }

        } else {
          // Manejar el error, por ejemplo, mostrando un mensaje al usuario
          this.modalMessageText.set('No se encontraron categorias para este usuario.');
          this.modalAlert.set(true);
        }
      });
    }


    //metodo para cargar los tipos de transacciones existentes
    cargarTiposTransacciones() {
      this.tipoTransaccionesServicio.getTransactionTypes().subscribe(response => {
        if (response.success) {
          this.tipoTransacciones.set(response.data ?? []);

          //si hay datos
          if(this.tipoTransacciones().length > 0){
            if(this.EntryType === 'OUT'){
              //filtrar categorias para mostrar solo las de naturaleza de gasto
              this.tipoTransactionSeleccionada.set(this.tipoTransacciones().filter(tipo => tipo.code === 'EXP')[0]);
            }

            if(this.EntryType === 'IN'){
              //filtrar categorias para mostrar solo las de naturaleza de ingreso
              this.tipoTransactionSeleccionada.set(this.tipoTransacciones().filter(tipo => tipo.code === 'INC')[0]);
            }
            console.log('Tipo de transacción seleccionada después de cargar los tipos:', this.tipoTransactionSeleccionada());

          }




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
    onAccountChange(accountId: string) {


      this.transactionForm.patchValue({
        fromAccountId: this.EntryType === 'OUT' ? accountId : null,
        toAccountId: this.EntryType === 'IN' ? accountId : null,
        transactionTypeId: this.tipoTransactionSeleccionada().transactionTypeId ?? null,
        userId: this.AuthServicio.userId(),
        dateTransaction: new Date() // Establecer la fecha actual al seleccionar una cuenta
      });
    }

    //evento para cuando se selecciona una categoria en el dropdown de categorias
    onCategoryChange(categoryId: string) {
      this.transactionForm.patchValue({
        categoryId: categoryId,
        transactionTypeId: this.tipoTransactionSeleccionada().transactionTypeId ?? null,
        userId: this.AuthServicio.userId(),
        dateTransaction: new Date() // Establecer la fecha actual al seleccionar una categoría
      });
    }

    //#endregion

    //#region acciones


    //evento para cuando se envía el formulario para agregar una nueva transacción
    onSubmit() {
      if (this.transactionForm.valid) {
        const formValue = this.transactionForm.value as TransactionRequestDTO;
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
