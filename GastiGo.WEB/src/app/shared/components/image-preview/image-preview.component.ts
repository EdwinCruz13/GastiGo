import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-image-preview',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './image-preview.component.html'
})
export class ImagePreviewComponent {
  @Input() src?: string;

  @Input() width: number = 80;
  @Input() height: number = 80;

  @Input() fallback: string = 'https://via.placeholder.com/100x100?text=No+Image';

  isValidImage: boolean = true;

  onError() {
    this.isValidImage = false;
  }

  ngOnChanges() {
    this.isValidImage = true;
  }


}
