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

  onChange(value: string) {
    this.internalValue = value;
    this.valueChange.emit(value);
  }

  private formatDate(value: Date | string | null): string | null {
    if (!value) return null;
    const date = typeof value === 'string' ? new Date(value) : value;
    return date.toISOString().split('T')[0]; // yyyy-MM-dd
  }

  
}