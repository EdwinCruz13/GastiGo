import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardYear } from '@core/models/dashboard/dashboard.model';
import { AuthService } from '@core/services/auth/auth.service';
import { DashboardService } from '@core/services/dashboard/dashboard.service';


import { ModalComponent } from '@shared/components/modal/modal.component';
import { getCategoryTotal, getGroupTotal, MONTHS } from '@core/utils/month.helper';
import { FinancialTableComponent } from '@features/dashboard/components/financial-table/financial-table.component';
import { LineChartComponent } from '@shared/components/chart/linechart/line-chart.component';
import { BarChartComponent } from '@shared/components/chart/bar-chart/bar-chart.component';
import { FinancialHealthComponent } from '../components/financial-health/financial-health.component';



type ExpenseStatus = 'good' | 'warning' | 'danger';
type SavingsStatus = 'good' | 'danger';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.page.html',
  styleUrls: ['./dashboard.page.css'],
  imports: [CommonModule, ModalComponent, FinancialTableComponent, LineChartComponent, BarChartComponent, FinancialHealthComponent],
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

    // 1. Construir lista con total por categoría
    const topCategories = expenses.categories
      .map(c => ({
        name: c.name,
        total: Math.abs(
          c.values.reduce((acc, v) => acc + v.amount, 0)
        )
      }))
      // 2. ordenar de mayor a menor
      .sort((a, b) => b.total - a.total)
      // 3. tomar solo top 10
      .slice(0, 10);

    const labels = topCategories.map(c => c.name);
    const data = topCategories.map(c => c.total);

    return {
      labels,
      datasets: [
        {
          label: 'Gastos por categoría',
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



  // Variable computada para obtener el estado de salud financiera
  totalIncome = computed(() => {
    const groups = this.incomeAndExpenses()?.groups ?? [];
    const income = groups.find(g => g.name === 'Income');

    if (!income) return 0;

    return income.categories.reduce((acc, c) =>
      acc + c.values.reduce((a, v) => a + v.amount, 0)
    , 0);
  });

  // Total de gastos es positivo para mostrar en la tarjeta, aunque internamente se guarde como negativo
  totalExpenses = computed(() => {
    const groups = this.incomeAndExpenses()?.groups ?? [];
    const expenses = groups.find(g => g.name === 'Expenses');



    if (!expenses) return 0;

    return Math.abs(expenses.categories.reduce((acc, c) =>
      acc + c.values.reduce((a, v) => a + v.amount, 0)
    , 0));
  });

  // Total de ahorros
  totalSavings = computed(() => {
    //mes actual
    const currentMonth = new Date().getMonth() + 1;

    const groups = this.savings()?.groups ?? [];
    const savings = groups.find(g => g.name === 'Savings')?.categories.reduce((acc, c) =>
      acc + c.values.reduce((a, v) => {
        if (v.month === currentMonth) {
          a += v.amount;
        }
        return a;
      }, 0)
    , 0) ?? 0;

    return savings;
  });


  totalSavingsNeto = computed(() => {
    //mes actual
    const currentMonth = new Date().getMonth() + 1;
    const groups = this.savings()?.groups ?? [];

    const savings = groups.find(g => g.name === 'Savings')?.categories.reduce((acc, c) =>
      acc + c.values.reduce((a, v) => {
        if (v.month === currentMonth) {
          a += v.amount;
        }
        return a;
      }, 0)
    , 0) ?? 0;

    return savings;
  });


  // Neto total (ingresos - gastos + ahorros)
  prevSavings = computed(() => {
    //mes actual
    const currentMonth = new Date().getMonth() + 1;
    //mes anterior
    const prevMonth = currentMonth === 1 ? 12 : currentMonth - 1;

    // ahorros del mes anterior
    const groups = this.savings()?.groups ?? [];

    //solo el monto del ahorro del mes anterior, no acumulado
    const savings = groups.find(g => g.name === 'Savings')?.categories.reduce((acc, c) =>
      acc + c.values.reduce((a, v) => {
        if (v.month === prevMonth) {
          a += v.amount;
        }
        return a;
      }, 0)
    , 0) ?? 0;

    return savings;
  });

  // neto total (ingresos - gastos + ahorros)
  neto = computed(() => {
    //solo ingreso - gastos
    return this.totalIncome()- this.totalExpenses();
  });


  financialHealth = computed(() => {
    const income = this.totalIncome();
    const expenses = this.totalExpenses();
    const savings = this.totalSavings();
    const prevSavings = this.prevSavings();

    const expenseRatio = income > 0 ? expenses / income : 0;
    const savingsChange = prevSavings > 0 ? (savings - prevSavings) / prevSavings : 0;

    const expenseStatus: ExpenseStatus =
      expenseRatio >= 0.7 ? 'danger' :
      expenseRatio >= 0.5 ? 'warning' : 'good';

    const savingsStatus: SavingsStatus =
      savingsChange < 0 ? 'danger' : 'good';

      console.log('Financial Health Computation:');
      console.log('Income:', income);
      console.log('Expenses:', expenses);
      console.log('Savings:', savings);
      console.log('Previous Savings:', prevSavings);
      console.log('Expense Ratio:', expenseRatio);

    return {
      expenseRatio,
      savingsChange,
      expenseStatus,
      savingsStatus
    };
  });

  financialHealthData = computed(() => ({
    ...this.financialHealth(),
    neto: this.neto()
  }));



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
