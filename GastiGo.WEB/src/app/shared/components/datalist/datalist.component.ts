import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-data-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './datalist.component.html'
})
export class DataListComponent<T = any> {

  // puede ser item o lista
  @Input({ required: true }) data!: T | T[];

  // campos dinámicos (soporta nested)
  @Input({ required: true }) titleField!: string;
  @Input() descriptionField?: string;
  @Input() valueField?: string;


  // progreso (opcional)
  @Input() headerTitle?: string;
  @Input() symbolField?: string; // opcional
  @Input() typeField?: string;   // "I" o "E"

  // acciones
  @Input() actions: ('view' | 'edit' | 'delete')[] = [];

  // eventos
  @Output() view = new EventEmitter<T>();
  @Output() edit = new EventEmitter<T>();
  @Output() delete = new EventEmitter<T>();

  // =========================
  // HELPERS
  // =========================

  isArray(data: T | T[]): data is T[] {
    return Array.isArray(data);
  }

  getValue(item: any, field?: string): any {
    if (!item || !field) return null;

    return field.split('.').reduce((obj, key) => obj?.[key], item);
  }

  hasField(item: any, field?: string): boolean {
    const value = this.getValue(item, field);
    return value !== null && value !== undefined && value !== '';
  }



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



  trackByFn(index: number, item: any): any {
    return item?.id || item?.accountId || index;
  }
}