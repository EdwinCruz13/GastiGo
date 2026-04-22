import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardYear } from '@core/models/dashboard/dashboard.model';
import { AuthService } from '@core/services/auth/auth.service';
import { DashboardService } from '@core/services/dashboard/dashboard.service';


import { ModalComponent } from '@shared/components/modal/modal.component';
import { getCategoryTotal, getGroupTotal, MONTHS } from '@core/utils/month.helper';
import { FinancialTableComponent } from '@shared/components/financial-table/financial-table.component/financial-table.component';
import { LineChartComponent } from '@shared/components/chart/linechart/line-chart.component';
import { BarChartComponent } from '@shared/components/chart/bar-chart/bar-chart.component';




@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.page.html',
  styleUrls: ['./dashboard.page.css'],
  imports: [ ModalComponent, FinancialTableComponent , LineChartComponent, BarChartComponent , CommonModule],
  standalone: true
})
export class DashboardPage implements OnInit {
  private dashboardService = inject(DashboardService);
  private authService = inject(AuthService);

  // Variable para almacenar el ID del usuario logueado
  userID = signal<string>('');
  // Variable para almacenar los datos del dashboard del año
  incomeAndExpenses = signal<DashboardYear | null>(null);
  savings = signal<DashboardYear | null>(null);
  investments = signal<DashboardYear | null>(null);

   // Variable para mostrar un mensaje de éxito después de guardar una categoría.
   modalAlert = signal(false);
   modalMessageText = signal("");

   MONTHS = MONTHS;

//#region "Gráficos"
  // Variable computada para obtener los totales mensuales de ingresos, gastos y ahorros
  IncomeExpenseslineData = computed(() => {

    const incomeGroups = this.incomeAndExpenses()?.groups ?? [];
    const savingsGroups = this.savings()?.groups ?? [];

    //combinar ambos señales
    const groups = [...incomeGroups, ...savingsGroups];

    const getTotals = (name: string) => {
      const g = groups.find(x => x.name === name);
      if (!g) return Array(12).fill(0);

      const totals = Array(12).fill(0);

      (g.categories ?? []).forEach(c => {
        (c.values ?? []).forEach(v => {
          totals[v.month - 1] += v.amount;
        });
      });

      return totals;
    };

    const income = getTotals('Income');
    const expenses = getTotals('Expenses').map(v => Math.abs(v)); //convertir a positivo para mostrar en el gráfico
    const savings = getTotals('Savings');

    return [
      { label: 'Income', data: income, color: '#22c55e' },
      { label: 'Expenses', data: expenses, color: '#ef4444' },
      { label: 'Savings', data: savings, color: '#a855f7' }
    ];
  });

  // Variable computada para obtener los totales mensuales de gastos por categoría
  expensesBarData = computed(() => {
    const groups = this.incomeAndExpenses()?.groups ?? [];
    const expenses = groups.find(g => g.name === 'Expenses');

    if (!expenses) {
      return {
        labels: [],
        datasets: []
      };
    }

    const labels = expenses.categories.map(c => c.name);

    const data = expenses.categories.map(c =>
      Math.abs(c.values.reduce((acc, v) => acc + v.amount, 0))
    );

    return {
      labels,
      datasets: [
        {
          label: 'Expenses',
          data,
          color: '#ef4444'
        }
      ]
    };
  });

  // Variable computada para obtener los totales mensuales netos (ingresos - gastos + ahorros)
  NetLineData = computed(() => {
    const groups = [
      ...(this.incomeAndExpenses()?.groups ?? []),
      ...(this.savings()?.groups ?? []),
      ...(this.investments()?.groups ?? [])
    ];

    const getTotals = (name: string) => {
      const g = groups.find(x => x.name === name);
      if (!g) return Array(12).fill(0);

      const totals = Array(12).fill(0);

      (g.categories ?? []).forEach(c => {
        (c.values ?? []).forEach(v => {
          totals[v.month - 1] += v.amount;
        });
      });

      return totals;
    };

    const income = getTotals('Income');
    const expenses = getTotals('Expenses'); // negativos
    const savings = getTotals('Savings');
    const investment = getTotals('Investment');

    // SOLO NETO
    const net = income.map((v, i) =>
      v + savings[i] + investment[i] + expenses[i]
    );

    return [
      {
        label: 'Net',
        data: net,
        color: '#3b82f6' // azul
      }
    ];
  });


//endregion





  // Variable para almacenar los datos del dashboard del año
  ngOnInit(): void {
    this.loadUser();
  }


  // Método para obtener los datos del dashboard del año
  loadIncomeExpenses() {
    this.dashboardService.getIncomeAndExpensesByUserAndYear(this.userID(), 2026)
      .subscribe(res => {
        this.incomeAndExpenses.set(res.data ?? null);
      });
  }

  loadSavings() {
    this.dashboardService.getSavingsByUserAndYear(this.userID(), 2026)
      .subscribe(res => {
        this.savings.set(res.data ?? null);
      });
  }

  loadInvestments() {
    this.dashboardService.getInvestmentsByUserAndYear(this.userID(), 2026)
      .subscribe(res => {
        this.investments.set(res.data ?? null);
      });
  }

  // Método para cargar el ID del usuario logueado
  loadUser() {
    const id = this.authService.userId() ?? '';
    this.userID.set(id);

    if (id) {
      this.loadIncomeExpenses();
      this.loadSavings();
      this.loadInvestments();
    }
  }



}
