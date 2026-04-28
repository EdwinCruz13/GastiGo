import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-financial-health',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './financial-health.component.html',
  styleUrls: ['./financial-health.component.css']
})
export class FinancialHealthComponent {

  @Input({ required: true }) data!: {
    expenseRatio: number;
    savingsChange: number;
    expenseStatus: 'good' | 'warning' | 'danger';
    savingsStatus: 'good' | 'danger';
    neto: number;
  };

}
