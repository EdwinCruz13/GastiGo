import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './modal.component.html'
})
export class ModalComponent {

  @Input() open = false;

  @Output() close = new EventEmitter<void>();

  onClose() {
    this.close.emit();
  }

}
