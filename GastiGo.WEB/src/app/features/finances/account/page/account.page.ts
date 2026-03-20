import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Component, inject, OnInit, signal } from '@angular/core';
import { AccountType } from '@core/models/finances/accountType.model';
import { Bank } from '@core/models/finances/bank.model';
import { Currency } from '@core/models/finances/currency.model';
import { AuthService } from '@core/services/auth/auth.service';
import { AccountTypeService } from '@core/services/finances/account-type.service';
import { AccountService } from '@core/services/finances/account.service';
import { BankService } from '@core/services/finances/bank.service';
import { CurrencyService } from '@core/services/finances/currency.service';
import { Account, AccountRequestDTO } from '@core/models/finances/account.model';
import { ModalComponent } from '@shared/components/modal/modal.component';
import { CommonModule } from '@angular/common';
import { DropdownSelectComponent } from '@shared/components/dropdown-select/dropdown-select.component';
import { CardComponent } from '@shared/components/card/card.component';

@Component({
  selector: 'app-account.page',
  imports: [CommonModule, ReactiveFormsModule, ModalComponent, DropdownSelectComponent, CardComponent],
  templateUrl: './account.page.html'
})
export class AccountPage implements OnInit {
  //#region declarar variables
  private cuentasServicio = inject(AccountService);
  private tipoCuentaServicio = inject(AccountTypeService);
  private AuthServicio = inject(AuthService);
  private monedasServicio = inject(CurrencyService);
  private bancoServicio = inject(BankService);

  //listados
  cuentas = signal<Account[] | []>([]);
  tipoCuentas = signal<AccountType[] | []>([]);
  monedas = signal<Currency[] | []>([]);
  bancos = signal<Bank[] | []>([]);


  // Variables para controlar el estado del modal del formulario de categoría.
  modalFormOpen = signal(false);
  // variable para obtener el usuario logueado
  userID = signal<string>('');
  // Variable para controlar si el formulario es para crear una nueva categoría o editar una existente.
  isNew = signal(true);
  // variable para almacenar los errores
  apiErrors: string[] = [];

  // Variable para mostrar un mensaje de éxito después de guardar una categoría.
  modalAlert = signal(false);
  modalMessageText = signal("");

  // Variable para almacenar el formulario de cuenta.
  formBuilder = inject(FormBuilder);
  accountForm = this.formBuilder.group({
    accountTypeId: [null as string | null],
    userId: [this.AuthServicio.userId()],
    currencyId: [null as string | null],
    bankId: [null as string | null],
    name: ['', [Validators.required, Validators.nullValidator]],
    description: ['', [Validators.required, Validators.nullValidator, Validators.maxLength(120)]],
    balance: [0, [Validators.required, Validators.nullValidator, Validators.min(0)]]
  });

  //#endregion


  //#region metodos para cargar datos
  ngOnInit() {
    this.CargarTipoCuentas();
    this.CargarMonedas();
    this.CargarBancos();
    this.CargarCuentas();
  }

  /// Método para cargar los tipos de cuentas
  CargarTipoCuentas() {
    this.tipoCuentaServicio.getAccountTypes().subscribe({
      next: (response) => {
        if (response.success) {
          this.tipoCuentas.set(response.data ?? []);
        } else {
          this.modalMessageText.set(response.message || 'Error al cargar los tipos de cuenta.');
          this.modalAlert.set(true);
        }
      },
      error: (error) => {
        this.modalMessageText.set('Error al cargar los tipos de cuenta.');
        this.modalAlert.set(true);
      }
    });
  }

  /// Método para cargar las monedas
  CargarMonedas() {
    this.monedasServicio.getCurrencies().subscribe({
      next: (response) => {
        if (response.success) {
          this.monedas.set(response.data ?? []);
        } else {
          this.modalMessageText.set(response.message || 'Error al cargar las monedas.');
          this.modalAlert.set(true);
        }
      },
      error: (error) => {
        this.modalMessageText.set('Error al cargar las monedas.');
        this.modalAlert.set(true);
      }
    });
  }

  /// Método para cargar los bancos
  CargarBancos() {
    this.bancoServicio.getBanks().subscribe({
      next: (response) => {
        if (response.success) {
          this.bancos.set(response.data ?? []);
        } else {
          this.modalMessageText.set(response.message || 'Error al cargar los bancos.');
          this.modalAlert.set(true);
        }
      },
      error: (error) => {
        this.modalMessageText.set('Error al cargar los bancos.');
        this.modalAlert.set(true);
      }
    });
  }

  /// Método para cargar las cuentas del usuario logueado
  CargarCuentas() {
    const userId = this.AuthServicio.userId();
    if (userId) {
      this.cuentasServicio.getAccounts(userId).subscribe({
        next: (response) => {
          if (response.success) {
            console.log('Cuentas cargadas:', response.data);
            this.cuentas.set(response.data ?? []);
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
    } else {
      this.modalMessageText.set('Usuario no autenticado. Por favor, inicie sesión.');
      this.modalAlert.set(true);
    }
  }

  //#endregion


  //#region métodos para manejar eventos del card

  abrirModalNuevaCuenta() {
    this.accountForm.reset();
    this.accountForm.patchValue({
      accountTypeId: null as string | null,
      userId: this.AuthServicio.userId(),
      currencyId: null as string | null,
      bankId: null as string | null,
      name: "",
      description: "",
      balance: 0
    });


    this.isNew.set(true);
    this.modalFormOpen.set(true);
  }

  // Método para manejar la acción de ver una cuenta desde el card.
  viewAccount(account: Account) {
    // Aquí puedes implementar la lógica para editar la cuenta seleccionada.
    // Por ejemplo, podrías abrir un modal con un formulario prellenado con los datos de la cuenta.
    this.accountForm.patchValue({
      accountTypeId: account.accountType.accountTypeId,
      userId: account.user.userId,
      currencyId: account.currency.currencyId,
      bankId: account.bank.bankId,
      name: account.name,
      description: account.description,
      balance: account.balance
    });

    this.isNew.set(false);
    this.modalFormOpen.set(true);
  }

  //#endregion

  //#region métodos para manejar cambios en los dropdowns

  // Método para manejar el cambio de selección en el dropdown de banco
  onBankChange(value: string) {
   this.accountForm.patchValue({
      bankId: value
    });
  }

  // Método para manejar el cambio de selección en el dropdown de tipo de cuenta
  onTypeAccountChange(value: string) {
    this.accountForm.patchValue({
      accountTypeId: value
    });
  }

  // Método para manejar el cambio de selección en el dropdown de moneda
  onCurrencyChange(value: string) {
    this.accountForm.patchValue({
      currencyId: value
    });
  }
  //#endregion

  //#region operaciones de CRUD
  onSubmit() {
    if (this.accountForm.valid) {
      if (this.isNew()) {
        this.saveAccount();
      }

      else {

      }
    }
    else {
      this.modalMessageText.set('Por favor, complete todos los campos requeridos correctamente.');
      this.modalAlert.set(true);
    }
  }

  // Método para guardar una nueva cuenta
  saveAccount() {
    const accountData = this.accountForm.value as AccountRequestDTO;
    this.cuentasServicio.create(accountData).subscribe({
      next: (response) => {
        if (response.success) {
          // Cerrar el modal y mostrar un mensaje de éxito
          this.modalFormOpen.set(false);

          this.modalMessageText.set('Cuenta creada exitosamente.');
          this.modalAlert.set(true);
          this.accountForm.reset();
        } else {
          this.modalMessageText.set(response.message || 'Error al crear la cuenta.');
          this.modalAlert.set(true);
        }
      },
      error: (error) => {
        this.modalMessageText.set('Error al crear la cuenta.');
        this.modalAlert.set(true);
      }
    });
  }
  //#endregion

}
