import { Component, Input, Output, EventEmitter, OnChanges, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-table',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './table.component.html'
})
export class TableComponent implements OnChanges, OnInit {

  @Input() data: any[] = [];
  @Input() actions: ('view' | 'edit' | 'delete')[] = [];

  @Output() view = new EventEmitter<any>();
  @Output() edit = new EventEmitter<any>();
  @Output() delete = new EventEmitter<any>();

  columns: string[] = [];

  // buscador
  searchTerm: string = '';

  // paginación
  pageSize: number = 25;
  currentPage: number = 1;

  filteredData: any[] = [];
  paginatedData: any[] = [];

  ngOnChanges() {
    if (this.data?.length > 0) {
      this.columns = Object.keys(this.data[0]).filter(x => x !== 'id');
    }
    this.applyFilter();
  }

  ngOnInit(): void {
    //console.log(this.actions);
  }

  //FILTRO GLOBAL
  applyFilter() {
    const term = this.searchTerm.toLowerCase();

    this.filteredData = this.data.filter(row =>
      this.columns.some(col =>
        String(row[col]).toLowerCase().includes(term)
      )
    );

    this.currentPage = 1;
    this.applyPagination();
  }

  //PAGINACIÓN
  applyPagination() {
    const start = (this.currentPage - 1) * this.pageSize;
    const end = start + this.pageSize;

    this.paginatedData = this.filteredData.slice(start, end);
  }

  totalPages(): number {
    return Math.ceil(this.filteredData.length / this.pageSize);
  }

  nextPage() {
    if (this.currentPage < this.totalPages()) {
      this.currentPage++;
      this.applyPagination();
    }
  }

  prevPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.applyPagination();
    }
  }

  onAction(action: string, row: any) {
    if (action === 'view') this.view.emit(row);
    if (action === 'edit') this.edit.emit(row);
    if (action === 'delete') this.delete.emit(row);
  }

  formatHeader(col: string): string {
    return col
      .replace(/([A-Z])/g, ' $1')
      .replace(/^./, str => str.toUpperCase());
  }

  /// formatear valores
  formatValue(value: any, col: string): any {
    // null o undefined
    if (value === null || value === undefined) return '';

    // si es número
    if (typeof value === 'number') {
      return new Intl.NumberFormat('es-NI', {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
      }).format(value);
    }

    // si es fecha
    if (value instanceof Date) {
      return new Intl.DateTimeFormat('es-NI').format(value);
    }

    // si viene como string numérico
    if (!isNaN(value) && value !== '') {
      return new Intl.NumberFormat('es-NI', {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
      }).format(Number(value));
    }

    return value;
  }
}
