import { Component, EventEmitter, Input, OnChanges, OnInit, Output, signal, SimpleChanges } from '@angular/core';
import { FormsModule } from "@angular/forms";

@Component({
  selector: 'app-dropdown-select',
  templateUrl: './dropdown-select.component.html',
  imports: [FormsModule]
})
/**
 * Componente generico para un dropdown select.
 * Permite configurar los campos de etiqueta y valor, asi como el placeholder y el valor seleccionado.
 * Emite un evento cuando el valor cambia.
 */
export class DropdownSelectComponent<T> implements OnChanges, OnInit {
  @Input() items: T[] | null = [];
  @Input() labelField!: keyof T;
  @Input() valueField!: keyof T;
  @Input() placeholder: string = 'Seleccione...';
  @Input() selectedValue: any;
  @Input() disabled: boolean = false;
  @Output() valueChange = new EventEmitter<any>();

  normalizedSelectedValue = '';
  uniqueId = signal<string>(crypto.randomUUID());

  ngOnInit(): void {
    this.normalizedSelectedValue = this.toOptionValue(this.selectedValue);
  }

  ngOnChanges(changes: SimpleChanges) {
    if ('selectedValue' in changes || 'items' in changes) {
      this.normalizedSelectedValue = this.toOptionValue(this.selectedValue);
    }
  }
  onChange(value: any) {
    if (value === '') value = null;
    if (value === 'true') value = true;
    if (value === 'false') value = false;

    this.valueChange.emit(value);
  }

  private toOptionValue(value: unknown): string {
    if (value === null || value === undefined) {
      return '';
    }

    return String(value);
  }
}
