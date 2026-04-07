import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-date-input',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './date-input.component.html',
  styleUrls: ['./date-input.component.css']
})
export class DateInputComponent implements OnChanges {

  @Input() label?: string;
  @Input() value: Date | string | null = null;
  @Input() min?: string;
  @Input() max?: string;

  @Output() valueChange = new EventEmitter<string>();

  internalValue: string | null = null;

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['value']) {
      this.internalValue = this.formatDate(this.value);
    }
  }

  // Emitir el valor formateado cuando el usuario cambie la fecha
  onChange(value: string) {
    this.internalValue = value;
    this.valueChange.emit(value);
  }

  // Formatea la fecha a yyyy-MM-dd para el input
  private formatDate(value: Date | string | null): string | null {
    if (!value) return null;

    // Si ya viene como yyyy-MM-dd, devolverlo tal cual
    if (typeof value === 'string') {
      if (/^\d{4}-\d{2}-\d{2}$/.test(value)) {
        return value;
      }

      const parsed = this.parseLocalDate(value);
      return parsed ? this.toInputDateFormat(parsed) : null;
    }

    return this.toInputDateFormat(value);
  }

  // Parsear fechas en formato dd/MM/yyyy o dd-MM-yyyy
  private parseLocalDate(value: string): Date | null {
    const parts = value.split(/[\/-]/);

    if (parts.length !== 3) return null;

    // Para formato dd/MM/yyyy
    const day = Number(parts[0]);
    const month = Number(parts[1]);
    const year = Number(parts[2]);

    if (!day || !month || !year) return null;

    return new Date(year, month - 1, day);
  }

  // Convertir una fecha a formato yyyy-MM-dd para el input
  private toInputDateFormat(date: Date): string {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');

    return `${year}-${month}-${day}`;
  }
}
