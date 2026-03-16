import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-dropdown-select',
  templateUrl: './dropdown-select.component.html'
})
/**
 * Componente genérico para un dropdown select.
 * Permite configurar los campos de etiqueta y valor, así como el placeholder y el valor seleccionado.
 * Emite un evento cuando el valor cambia.
 */
export class DropdownSelectComponent<T> {
  @Input() items: T[] | null = [];
  @Input() labelField!: keyof T;
  @Input() valueField!: keyof T;
  @Input() placeholder: string = 'Seleccione...';
  @Input() selectedValue: any;
  @Output() valueChange = new EventEmitter<any>();

  // Maneja el cambio de selección en el dropdown
  onChange(event: Event) {
    const select = event.target as HTMLSelectElement;
    this.valueChange.emit(select.value);

  }

}