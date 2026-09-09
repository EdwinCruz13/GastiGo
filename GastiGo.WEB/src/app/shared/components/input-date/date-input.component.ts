import {
  Component,
  EventEmitter,
  Input,
  Output,
  forwardRef
} from '@angular/core';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import {
  ControlValueAccessor,
  NG_VALUE_ACCESSOR
} from '@angular/forms';

@Component({
  selector: 'app-date-input',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './date-input.component.html',
  styleUrls: ['./date-input.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DateInputComponent),
      multi: true
    }
  ]
})
export class DateInputComponent implements ControlValueAccessor {

  @Input() label?: string;

  @Input() min?: string;
  @Input() max?: string;

  @Input() includeTime: boolean = false;

  // Para mantener compatibilidad con [value]
  @Input()
  set value(value: Date | string | null) {
    this.internalValue = this.formatDate(value);
  }

  // Para mantener compatibilidad con (valueChange)
  @Output() valueChange = new EventEmitter<string>();

  internalValue: string | null = null;

  disabled = false;

  private onChange: (value: string | null) => void = () => {};
  private onTouched: () => void = () => {};

  // Reactive Forms
  writeValue(value: Date | string | null): void {
    this.internalValue = this.formatDate(value);
  }

  registerOnChange(fn: (value: string | null) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  onValueChange(value: string): void {

    this.internalValue = value;

    // Para formControlName
    this.onChange(value);

    // Para [value] + (valueChange)
    this.valueChange.emit(value);
  }

  onBlur(): void {
    this.onTouched();
  }

  private formatDate(
    value: Date | string | null
  ): string | null {

    if (!value) {
      return null;
    }

    if (value instanceof Date) {
      return this.toInputFormat(value);
    }

    // yyyy-MM-dd
    if (
      !this.includeTime &&
      /^\d{4}-\d{2}-\d{2}$/.test(value)
    ) {
      return value;
    }

    // yyyy-MM-ddTHH:mm:ss
    if (
      this.includeTime &&
      /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}$/.test(value)
    ) {
      return value;
    }

    // Formato API / SQL
    if (this.includeTime) {

      const match = value.match(
        /^(\d{4}-\d{2}-\d{2})[T ](\d{2}:\d{2}:\d{2})/
      );

      if (match) {
        return `${match[1]}T${match[2]}`;
      }
    }

    const parsed = new Date(value);

    if (isNaN(parsed.getTime())) {
      return null;
    }

    return this.toInputFormat(parsed);
  }

  private toInputFormat(date: Date): string {

    const year = date.getFullYear();

    const month = String(
      date.getMonth() + 1
    ).padStart(2, '0');

    const day = String(
      date.getDate()
    ).padStart(2, '0');

    if (!this.includeTime) {
      return `${year}-${month}-${day}`;
    }

    const hours = String(
      date.getHours()
    ).padStart(2, '0');

    const minutes = String(
      date.getMinutes()
    ).padStart(2, '0');

    const seconds = String(
      date.getSeconds()
    ).padStart(2, '0');

    return `${year}-${month}-${day}T${hours}:${minutes}:${seconds}`;
  }
}
