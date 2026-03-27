import { Component, inject, signal, OnInit, Signal } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoryService } from '@core/services/finances/category.service';
import { NatureService } from '@core/services/finances/nature.service';

import { Category, CategoryRequestDTO } from '@core/models/finances/category.model';
import { Nature } from '@core/models/finances/nature.model';
import { TreeNode } from '@core/models/common/tree-node.model';

import { TreeNodeComponent } from '@shared/components/tree/tree-node.component';
import { ModalComponent } from '@shared/components/modal/modal.component';
import { DropdownSelectComponent } from '@shared/components/dropdown-select/dropdown-select.component';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '@core/services/auth/auth.service';
import { State } from '@core/models/common/state.model';

@Component({
  selector: 'app-category-tree',
  standalone: true,
  imports: [
    CommonModule,
    TreeNodeComponent,
    ModalComponent,
    DropdownSelectComponent,
    ReactiveFormsModule
  ],
  templateUrl: './category.page.html'
})
export class CategoryPage implements OnInit {
  /////////////////////////////////////////////////////////////////////////////////////////////////
  //#region Declaración de Variables y servicios
  ///////////////////////////////////////////////////////////////////////////////////////

  // Inyecta los servicios de categoría y naturaleza para interactuar con la API
  // y obtener datos relacionados con las categorías y naturalezas.
  private CategoriaServicio = inject(CategoryService);
  private NaturalezaServicio = inject(NatureService);
  private AuthServicio = inject(AuthService);

  // true si es nuevo, false si es edición
  isNew = signal(true); // Variable para controlar si se está creando una nueva categoría o editando una existente.

  tree = signal<TreeNode<Category>[] | null>([]);
  currentNode = signal<TreeNode<Category> | null>(null);

  natures = signal<Nature[] | null>([]);

  userID = signal<string>('');

  // Variables para controlar el estado del modal del formulario de categoría.
  modalFormOpen = signal(false);

  // Variable para controlar el estado del modal de confirmación de eliminación de categoría.
  modalDeleteOpen = signal(false);

  // Variable para mostrar un mensaje de éxito después de guardar una categoría.
  modalAlert = signal(false);
  modalMessageText = signal("");

  // Variable para almacenar los errores de la API y mostrarlos en la interfaz.
  apiErrors: string[] = [];

  // Variable para almacenar el estado de activación de la categoría.
  Estados = signal<State[]>([{ id: true, name: 'Activo' },{ id: false, name: 'Inactivo' }]);


  // Inyecta el servicio FormBuilder para crear el formulario de categoría con validaciones.
  formBuilder = inject(FormBuilder);
  categoryForm = this.formBuilder.group({
    parentId: [null as string | null], //campo de ID de categoría padre sin validación
    parentName: [{ value: '', disabled: true }],
    userId: [this.AuthServicio.userId()], //campo de ID de usuario sin validación, se establece con el ID del usuario autenticado
    natureId: [null as string | null, Validators.required], //campo de ID de naturaleza con validación de requerido
    name: ['', [Validators.required, Validators.nullValidator]], //campo de nombre con validación de requerido
    description: ['', [Validators.required, Validators.nullValidator, Validators.maxLength(120)]], //campo de descripción con validación de requerido y longitud mínima de 6 caracteres
    isActive: [null as boolean | null, Validators.required], //campo de activación con valor por defecto true
    isSalary: [null as boolean | null, Validators.required], //campo de salario con valor por defecto false
    applySalary: [false],
    applyPercentage: [false],
    applyAmount: [false],
    value: [0, [Validators.min(0)]]
  });


  //#endregion


  /////////////////////////////////////////////////////////////////////////////////////////////////
  //#region eventos load
  /////////////////////////////////////////////////////////////////////////////////////////////////

  // El método ngOnInit se ejecuta al inicializar el componente.
  // Aquí se establece el ID del usuario utilizando el servicio de
  // autenticación y se cargan
  // las categorías y naturalezas desde la API
  // para mostrar en la interfaz.
  ngOnInit() {


    this.CargarUsuario();
    this.CargarCategorias();
    this.CargarNaturalezas();


  }


  // Carga el ID del usuario desde el servicio de autenticación y lo almacena en una señal para su uso en la aplicación.
  CargarUsuario(){
    // Establece el ID del usuario utilizando el servicio de autenticación.
    const uid = this.AuthServicio.userId() ?? '';
    this.userID.set(uid);
  }

  // Carga las categorías desde el servicio y las mapea a una estructura de árbol para el componente TreeNodeComponent.
  CargarCategorias() {
    this.CategoriaServicio.getTree(this.userID())
      .subscribe({
        next: (response) => {
          const mapped = this.mapCategoryToTree(response.data ?? []);
          this.tree.set(mapped);
        },
        error: (err) => console.log(err)

      });
  }

  // Carga las naturalezas desde el servicio y las almacena en una señal para su uso en la interfaz.
  CargarNaturalezas() {
    this.NaturalezaServicio.getAll()
      .subscribe({
        next: (response) => {
          this.natures.set(response.data);
          //console.log("Naturalezas cargadas:", response.data);

        },
        error: (err) => console.log(err)
      });

  }

  //#endregion

  /////////////////////////////////////////////////////////////////////////////////////////////////
  //#region Eventos para el control del árbol de categorías
  /////////////////////////////////////////////////////////////////////////////////////////////////

  //anade una categoria raiz al formulario, seteando el parentID a null y el parentName a "Categoría raíz".
  addRootCategory() {
    this.isNew.set(true); // Indica que se está creando una nueva categoría raíz.

    //modifica la propiedad parentId del formulario a null y parentName a "Categoría raíz",
    // indicando que se está creando una categoría raíz sin un padre específico.
    this.categoryForm.reset();
    this.categoryForm.patchValue({
      parentId: null,
      parentName: 'Categoría raíz',
      isActive: true,
      isSalary: false,
      userId: this.userID(),
      applySalary: false,
      applyPercentage: false,
      applyAmount: false,
      value: 0
    });
    this.categoryForm.controls.parentName.disable();

    //abre el modal
    this.modalFormOpen.set(true);
  }

  //anade un node a la categoria
  addNodeCategory(node: TreeNode<Category>) {
    this.isNew.set(true); // Indica que se está creando una nueva categoría hija.
    this.currentNode.set(node); // Establece el nodo actual para su edición.
    this.categoryForm.reset();

    // Modifica la propiedad parentID del formulario al ID del nodo seleccionado y parentName al nombre del nodo,
    this.categoryForm.patchValue({
      parentId: node.id as string,
      parentName: node.label,
      userId: this.userID(),
      natureId: node.data?.nature.natureId,
      isActive: true,
      isSalary: false,
      applySalary: false,
      applyPercentage: false,
      applyAmount: false,
      value: 0
    });

    this.categoryForm.controls.parentName.disable();
    this.modalFormOpen.set(true);
  }

  //edita la categoria
  editNodeCategory(node: TreeNode<Category>) {
    this.isNew.set(false); // Indica que se está editando una categoría existente.
    this.currentNode.set(node); // Establece el nodo actual para su edición.

    // Busca el nodo padre en el árbol utilizando el ID del padre almacenado en los datos del nodo actual.
    const parentNode = this.findNodeById(this.tree() ?? [], node.data?.parentId);

    // Modifica los campos del formulario con los datos de la categoría seleccionada,
    // incluyendo el nombre, descripción, naturaleza y el ID del padre.
    this.categoryForm.patchValue({
      name: node.data?.name,
      description: node.data?.description,
      natureId: node.data?.nature.natureId,
      isActive: node.data?.isActive ?? null,
      isSalary: node.data?.isSalary ?? null,
      applySalary: node.data?.applySalary ?? false,
      applyPercentage: node.data?.applyPercentage ?? false,
      applyAmount: node.data?.applyAmount ?? false,
      value: node.data?.value ?? 0,
      parentId: node.data?.parentId ?? null,
      parentName: parentNode?.label ?? ""
    });

    this.categoryForm.updateValueAndValidity();
    this.categoryForm.controls.parentName.disable();
    this.modalFormOpen.set(true);

  }

  deleteCategory(node: TreeNode<Category>) {
    this.currentNode.set(node); // Establece el nodo actual para su eliminación.
    this.modalDeleteOpen.set(true); // Abre el modal de confirmación de eliminación.
  }

  //#endregion

  /////////////////////////////////////////////////////////////////////////////////////////////////
  //#region Eventos para el control del input del formulario de categoría
  /////////////////////////////////////////////////////////////////////////////////////////////////

  // Actualiza el campo natureID del formulario cuando se selecciona una nueva naturaleza en el dropdown.
  onCategoryChange(value: string) {
    this.categoryForm.patchValue({
      natureId: value
    });
  }

  onStateChange(value: boolean) {
    this.categoryForm.patchValue({
      isActive: value
    });
  }
  //#endregion

  /////////////////////////////////////////////////////////////////////////////////////////////////
  //#region Operaciones de CRUD
  /////////////////////////////////////////////////////////////////////////////////////////////////

  onSubmit() {
    // si es nuevo entonces guardar categoria
    if (this.isNew()) {
      this.saveCategory();
    }

    // si no es nuevo entonces actualizar categoria
    if (!this.isNew()) {
      this.updateCategory();
    }

  }


  // Crea una nueva categoría utilizando los datos proporcionados y llama al servicio para guardarla.
  // Luego recarga el árbol de categorías y cierra el modal.
  saveCategory() {
    // Verifica si el formulario es válido antes de proceder con la creación de la categoría.
    if (!this.categoryForm.valid) {
      this.categoryForm.markAllAsTouched();
      this.apiErrors = ["Formulario no válido. Corrige los errores e inténtalo de nuevo."];

      // Object.keys(this.categoryForm.controls).forEach(key => {
      //   const control = this.categoryForm.get(key);
      //   console.log(key, control?.errors);
      // });


      return;
    }

    try {
      // Crea un nuevo objeto de tipo CategoryRequestDTO
      // a partir de los valores del formulario.
      const newCategory = this.categoryForm.value as CategoryRequestDTO;
      console.log("Formulario a enviar:", newCategory);

      // Llamada al servicio para crear la categoría
      this.CategoriaServicio.create(newCategory)
        .subscribe({
          next: (response) => {
            this.CargarCategorias(); // Recargar el árbol después de crear la categoría

            // Resetea el formulario para limpiar los campos y establecer el userId nuevamente
            this.categoryForm.reset({
              userId: this.userID()
            });

            // Cerrar el modal después de crear la categoría
            this.modalFormOpen.set(false); // Cerrar el modal

            // Mostrar mensaje de éxito
            this.modalMessageText.set("¡Categoría creada con éxito!"); // Establece el mensaje de éxito para el modal
            this.modalAlert.set(true);
          },
          error: (err) => { console.log(err); this.apiErrors = err.error.errors ?? ["Error desconocido"]; }
        });
    } catch (err) {
      this.apiErrors = ["Error desconocido"];
    }
  }

  //actualiza una categoría existente utilizando los datos proporcionados y llama al servicio para actualizarla.
  updateCategory() {

    console.log("Formulario a enviar:", this.categoryForm.value, this.currentNode()?.data?.categoryId!);

    // Verifica si el formulario es válido antes de proceder con la actualización de la categoría.
    if (!this.categoryForm.valid) {
      this.categoryForm.markAllAsTouched();
      this.apiErrors = ["Formulario no válido. Corrige los errores e inténtalo de nuevo."];
      // Object.keys(this.categoryForm.controls).forEach(key => {
      //   const control = this.categoryForm.get(key);
      //   console.log(key, control?.errors);
      // });


      return;
    }

    try {
      // Crea un nuevo objeto de tipo CategoryRequestDTO
      // a partir de los valores del formulario.
      const updateCategory = this.categoryForm.value as CategoryRequestDTO;

      // Llamada al servicio para actualizar la categoría
      this.CategoriaServicio.update(this.currentNode()?.data?.categoryId!, updateCategory)
        .subscribe({
          next: (response) => {
            this.CargarCategorias(); // Recargar el árbol después de actualizar la categoría

            // Resetea el formulario para limpiar los campos y establecer el userId nuevamente
            this.categoryForm.reset({
              userId: this.userID()
            });

            // Cerrar el modal después de actualizar la categoría
            this.modalFormOpen.set(false); // Cerrar el modal

            // Mostrar mensaje de éxito
            this.modalMessageText.set("¡Categoría actualizada con éxito!"); // Establece el mensaje de éxito para el modal
            this.modalAlert.set(true);
          },
          error: (err) => { console.log(err); this.apiErrors = err.error.errors ?? ["Error desconocido"]; }
        });
    } catch (err) {
      this.apiErrors = ["Error desconocido"];
    }
  }

  // Elimina una categoría existente utilizando su ID y llama al servicio para eliminarla.
  deleteCategoryById() {
    const id: string = this.currentNode()?.data?.categoryId!;
    this.CategoriaServicio.delete(id)
      .subscribe({
        next: (response) => {
          this.CargarCategorias(); // Recargar el árbol después de eliminar la categoría

           // Cerrar el modal después de actualizar la categoría
            this.modalDeleteOpen.set(false); // Cerrar el modal

            // Mostrar mensaje de éxito
            this.modalMessageText.set("¡Categoría eliminada con éxito!"); // Establece el mensaje de éxito para el modal
            this.modalAlert.set(true);
        },
        error: (err) => { console.log(err); this.apiErrors = err.error.errors ?? ["Error desconocido"]; }
      });
  }

  //#endregion

  /////////////////////////////////////////////////////////////////////////////////////////////////
  //#region Utilidades
  /////////////////////////////////////////////////////////////////////////////////////////////////

  // Convierte una lista de categorías en una estructura de árbol compatible con el componente TreeNodeComponent.
  private mapCategoryToTree(categories: Category[]): TreeNode<Category>[] {
    return categories.map(cat => ({

      id: cat.categoryId ?? (cat as any).categoryID,
      label: cat.name,
      level: cat.level,
      type: cat.nature.abbre,
      isActive: cat.isActive,
      data: cat,
      children: this.mapCategoryToTree(cat.children ?? [])
    }));
  }

  // Busca un nodo en el árbol de categorías por su ID.
  // devuelve el nodo encontrado o null si no se encuentra.
  private findNodeById(nodes: TreeNode<Category>[],id: string | null | undefined): TreeNode<Category> | null {
    if (!id) return null;
    for (const node of nodes) {
      if (node.id === id) return node;

      const found = this.findNodeById(node.children ?? [], id);
      if (found) return found;
    }

    return null;
  }

  //#endregion

}
