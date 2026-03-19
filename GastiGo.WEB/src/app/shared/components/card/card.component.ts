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

  // ahora recibe item o lista
  @Input({ required: true }) data!: T | T[];

  // mapeo dinámico (tipo tabla)
  @Input({ required: true }) titleField!: keyof T;
  @Input({ required: true }) descriptionField!: keyof T;
  @Input() imageField?: keyof T;

  // campos opcionales
  @Input() badgeField?: keyof T;  // para mostrar un badge o etiqueta
  @Input() metaField?: keyof T; // para mostrar información adicional en el pie de la tarjeta

  // acciones
  @Input() actions: ('view' | 'edit' | 'delete')[] = [];

  @Output() view = new EventEmitter<T>();
  @Output() edit = new EventEmitter<T>();
  @Output() delete = new EventEmitter<T>();

  // helper
  isArray(data: T | T[]): data is T[] {
    return Array.isArray(data);
  }

  getValue(item: T, field: keyof T): any {
    return item?.[field];
  }

  onAction(action: 'view' | 'edit' | 'delete', item: T) {
    if (action === 'view') this.view.emit(item);
    if (action === 'edit') this.edit.emit(item);
    if (action === 'delete') this.delete.emit(item);
  }

  hasField(item: T, field?: keyof T): boolean {
    return !!field && !!item?.[field];
  }
}
