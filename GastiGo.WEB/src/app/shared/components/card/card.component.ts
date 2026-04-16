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

  // =========================
  // INPUTS
  // =========================

  // data puede ser item o lista
  @Input({ required: true }) data!: T | T[];

  // campos dinámicos (soportan nested: "currency.name")
  @Input() titleField?: string;
  @Input() descriptionField?: string;
  @Input() imageField?: string;
  @Input() badgeField?: string;
  @Input() metaField?: string;

  // valores estáticos
  @Input() title?: string;
  @Input() description?: string;
  @Input() badge?: string;
  @Input() meta?: string;
  @Input() isNumber = false;

  // acciones disponibles
  @Input() actions: ('view' | 'edit' | 'delete')[] = [];

  // =========================
  // OUTPUTS
  // =========================

  @Output() view = new EventEmitter<T>();
  @Output() edit = new EventEmitter<T>();
  @Output() delete = new EventEmitter<T>();

  // =========================
  // HELPERS
  // =========================

  // saber si es array
  isArray(data: T | T[]): data is T[] {
    return Array.isArray(data);
  }

  // obtener valores dinámicos (soporta "currency.name")
  getValue(item: any, field?: string): any {
    if (!item || !field) return null;
    return field.split('.').reduce((obj, key) => obj?.[key], item);
  }

  // resolver valor (prioridad: estático > dinámico)
  resolveValue(item: any, field?: string, staticValue?: any): any {
    if (staticValue !== undefined && staticValue !== null) return staticValue;
    return this.getValue(item, field);
  }

  // validar si existe el campo
  hasField1(item: any, field?: string): boolean {
    const value = this.getValue(item, field);
    return value !== null && value !== undefined && value !== '';
  }

  hasField(item: any, field?: string): boolean {
    const value = this.getValue(item, field);
    return value !== null && value !== undefined;
  }

  // =========================
  // ACTIONS
  // =========================

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

  // =========================
  // PERFORMANCE
  // =========================

  trackByFn(index: number, item: any): any {
    return item?.id || item?.accountId || index;
  }
}
