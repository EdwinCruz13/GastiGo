import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent<T = any> {

  //data puede ser item o lista
  @Input({ required: true }) data!: T | T[];

  //campos dinámicos (soportan nested: "currency.name")
  @Input({ required: true }) titleField!: string;
  @Input({ required: true }) descriptionField!: string;
  @Input() imageField?: string;
  @Input() badgeField?: string;
  @Input() metaField?: string;

  // acciones disponibles
  @Input() actions: ('view' | 'edit' | 'delete')[] = [];

  // eventos
  @Output() view = new EventEmitter<T>();
  @Output() edit = new EventEmitter<T>();
  @Output() delete = new EventEmitter<T>();

  // =========================
  // HELPERS
  // =========================

  //saber si es array
  isArray(data: T | T[]): data is T[] {
    return Array.isArray(data);
  }

  //obtener valores (soporta "currency.name")
  getValue(item: any, field?: string): any {
    if (!item || !field) return null;

    return field.split('.').reduce((obj, key) => obj?.[key], item);
  }

  //validar si existe el campo
  hasField(item: any, field?: string): boolean {
    const value = this.getValue(item, field);
    return value !== null && value !== undefined && value !== '';
  }

  // acciones
  onAction(action: 'view' | 'edit' | 'delete', item: T) {
    switch (action) {
      case 'view':
        this.view.emit(item);
        break;
      case 'edit':
        this.edit.emit(item);
        break;
      case 'delete':
        this.delete.emit(item);
        break;
    }
  }

  // ✔️ opcional (mejora rendimiento en listas grandes)
  trackByFn(index: number, item: any): any {
    return item?.id || item?.accountId || index;
  }
}
