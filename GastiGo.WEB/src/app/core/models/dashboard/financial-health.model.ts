export interface FinancialHealth {
  expenseRatio: number;
  savingsChange: number;
  expenseStatus: 'good' | 'warning' | 'danger';
  savingsStatus: 'good' | 'danger';
  neto: number;
}
