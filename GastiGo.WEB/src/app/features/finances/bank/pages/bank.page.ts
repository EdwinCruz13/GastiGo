import { Bank, BankRequestDTO } from '@core/models/finances/bank.model';
import { BankService } from '@core/services/finances/bank.service';
import { Component, inject, OnInit, signal } from '@angular/core';

import { ModalComponent } from '@shared/components/modal/modal.component';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CardComponent } from '@shared/components/card/card.component';
import { ImagePreviewComponent } from '@shared/components/image-preview/image-preview.component';
import { DataListComponent } from '@shared/components/datalist/datalist.component';

@Component({
  selector: 'app-bank.page',
  imports: [ReactiveFormsModule, ModalComponent, CardComponent, ImagePreviewComponent],
  templateUrl: './bank.page.html'
})
export class BankPage implements OnInit{
  BankServicio = inject(BankService);
  banks = signal<Bank[] | []>([]);
  selectedBank = signal<Bank | null>(null);

  // Variable para controlar la apertura del modal de formulario.
  modalFormOpen = signal(false);

  // Variables para el modal de alerta.
  modalAlert = signal(false);
  modalMessageText = signal("");

  isNew = signal(true); // Variable para determinar si se está creando un nuevo banco o editando uno existente.
  title = signal("Nuevo banco"); // Título del modal, cambia según la acción (nuevo o editar).

  // Variable para almacenar los errores de la API y mostrarlos en la interfaz.
  apiErrors: string[] = [];


    // Inyecta el servicio FormBuilder para crear el formulario de categoría con validaciones.
  formBuilder = inject(FormBuilder);
  bankForm = this.formBuilder.group({
    name: ['', [Validators.required, Validators.nullValidator]], //campo de nombre del banco con validación de requerido
    abbre: ['', [Validators.required, Validators.nullValidator, Validators.maxLength(120)]], //campo de descripción del banco con validación de requerido y longitud máxima de 120 caracteres
    transferFee: [1, [Validators.required, Validators.nullValidator, Validators.min(0.1)]], //campo de tarifa de transferencia con validación de requerido y valor mínimo de 0
    imgURL: [''] //campo opcional para la URL de la imagen del banco sin validaciones
  });




  //#region Load

  ngOnInit() {
    this.CargarBancos();
  }

  // Método para cargar la lista de bancos desde el servicio y actualizar la señal de bancos.
  CargarBancos() {
    this.BankServicio.getBanks()
      .subscribe({
        next: (response) => {
         if (response.success) {
            this.banks.set(response.data ?? []);
          } else {
            // Manejar el error
            this.modalMessageText.set("No se pudieron cargar los bancos. Intente nuevamente más tarde.");
            this.modalAlert.set(true);
          }
        },
        error: (err) => {
          // Manejar el error de la solicitud HTTP
          this.modalMessageText.set("Ocurrió un error al cargar los bancos. Intente nuevamente más tarde.");
          this.modalAlert.set(true);
        }

      });
  }
  //#endregion


  //#region Acciones para controlar el modal de formulario y manejar las acciones de agregar, editar y eliminar bancos.

  // Método para abrir el modal de formulario para agregar un nuevo banco.
  addBank() {
    // Establece el título del modal y la variable isNew para indicar que se está creando un nuevo banco.
    this.title.set("Nuevo banco");
    this.isNew.set(true);


    this.bankForm.reset();
    this.bankForm.patchValue({
      name: "",
      abbre: "",
      transferFee: 1,
      imgURL: ""
    });

    //abre el modal
    this.modalFormOpen.set(true);

  }


  editBank(bank: Bank) {
    this.selectedBank.set(bank);
    console.log('Edit bank:', bank);

    // Establece el título del modal y la variable isNew para indicar que se está editando un banco existente.
    this.title.set("Editar banco");
    this.isNew.set(false);


    this.bankForm.reset();
    this.bankForm.patchValue({
      name: bank.name,
      abbre: bank.abbre,
      transferFee: bank.transferFee,
      imgURL: bank.imgURL
    });

    //abre el modal
    this.modalFormOpen.set(true);
  }

  deleteBank(bank: Bank) {
    console.log('Delete bank:', bank);
  }
  //#endregion

  // Método para manejar el envío del formulario de banco.
  onSubmit() {

    // si es un nuevo banco, se llama al método SaveBank para guardar el nuevo banco.
    if (this.isNew()) {
      this.SaveBank();
    }

    // Si no es un nuevo banco, se puede implementar la lógica para actualizar el banco existente.
    if (!this.isNew()) {
      this.UpdateBank();
    }
  }


  // Método para guardar un nuevo banco, verifica si el formulario es válido antes de procesar los datos.
  SaveBank() {
    // Verifica si el formulario es válido antes de procesar los datos.
    if (this.bankForm.valid) {
      // Si el formulario es válido, se crea un objeto BankRequestDTO con los datos del formulario y se llama al servicio para crear el banco.
      const bankData = this.bankForm.value as BankRequestDTO;

      // Llama al servicio para crear el banco y maneja la respuesta.
      this.BankServicio.create(bankData)
        .subscribe({
          next: (response) => {
            this.CargarBancos(); // Recarga la lista de bancos después de crear uno nuevo.
            this.bankForm.reset(); // Limpia el formulario después de guardar.

            this.modalFormOpen.set(false); // Cierra el modal de formulario después de guardar.

            // Muestra el modal de alerta con el mensaje de éxito.
            this.modalMessageText.set("Banco creado exitosamente."); // Establece el mensaje de éxito para mostrar en el modal de alerta.
            this.modalAlert.set(true);

          },
          error: (err) => {

            this.apiErrors = err.error?.errors || ['Error al crear el banco. Por favor, inténtalo de nuevo.'];
            this.modalMessageText.set("Error al crear el banco. Por favor, inténtalo de nuevo.");
            this.modalAlert.set(true);
          }
        });

    } else {
      this.bankForm.markAllAsTouched();
      this.apiErrors = ["Formulario no válido. Corrige los errores e inténtalo de nuevo."];

      // Si el formulario no es válido, muestra un mensaje de error en el modal de alerta.
      this.modalMessageText.set("Por favor, completa todos los campos requeridos correctamente.");
      this.modalAlert.set(true);
    }
  }

  //Metodo para actualizar un banco existente, verifica si el formulario es válido antes de procesar los datos.
  UpdateBank() {
    // Aquí puedes implementar la lógica para actualizar un banco existente.
    const bankData = this.bankForm.value as BankRequestDTO;
    const bankId = this.selectedBank()?.bankId;

    if (bankId) {
      this.BankServicio.update(bankId, bankData)
        .subscribe({
          next: (response) => {
            this.CargarBancos(); // Recarga la lista de bancos después de actualizar uno existente.
            this.bankForm.reset(); // Limpia el formulario después de actualizar.

            this.modalFormOpen.set(false); // Cierra el modal de formulario después de actualizar.

            // Muestra el modal de alerta con el mensaje de éxito.
            this.modalMessageText.set("Banco actualizado exitosamente."); // Establece el mensaje de éxito para mostrar en el modal de alerta.
            this.modalAlert.set(true);

          },
          error: (err) => {

            this.apiErrors = err.error?.errors || ['Error al actualizar el banco. Por favor, inténtalo de nuevo.'];
            this.modalMessageText.set("Error al actualizar el banco. Por favor, inténtalo de nuevo.");
            this.modalAlert.set(true);
          }
        });
    }
  }




}
