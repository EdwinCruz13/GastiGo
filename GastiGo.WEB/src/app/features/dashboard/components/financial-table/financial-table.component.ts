import { Component, Input, OnChanges, SimpleChanges, computed, signal } from '@angular/core';
import { AfterViewInit, ElementRef, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardYear } from '@core/models/dashboard/dashboard.model';

@Component({
  selector: 'app-financial-table',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './financial-table.component.html',
  styleUrls: ['./financial-table.component.css']
})
export class FinancialTableComponent implements OnChanges, AfterViewInit  {

  @Input({ required: true }) data!: DashboardYear | null;
  @Input() months: string[] = [];
  @Input() showTotals = true;
  @Input() showNetFlow = true;
  @Input() showAccumulated = true;

  @ViewChild('tableWrapper') tableWrapper!: ElementRef<HTMLDivElement>;


  dataSignal = signal<DashboardYear | null>(null);
  currentMonthIndex = -1;



  ngOnChanges(changes: SimpleChanges): void {
    if (changes['data']) {
      this.dataSignal.set(this.data);
    }
  }

  ngAfterViewInit() {
    setTimeout(() => this.scrollToCurrentMonth(), 100);
  }

  ngAfterViewChecked() {
    this.scrollToCurrentMonth();
  }


  // =========================
  // COMPUTEDS INTERNOS
  // =========================

  categoryTotals = computed(() => {
    if (!this.data?.groups) return {};

    const result: Record<string, number> = {};

    this.data.groups.forEach(g => {
      (g.categories ?? []).forEach(cat => {
        result[cat.name] = (cat.values ?? [])
          .reduce((acc, v) => acc + v.amount, 0);
      });
    });

    return result;
  });

  groupTotals = computed(() => {
    if (!this.data?.groups) return {};

    const result: Record<string, number> = {};

    this.data.groups.forEach(g => {
      result[g.name] = (g.categories ?? []).reduce((acc, cat) => {
        return acc + (cat.values ?? [])
          .reduce((a, v) => a + v.amount, 0);
      }, 0);
    });

    return result;
  });

  monthTotals = computed(() => {
    const data = this.dataSignal();


    if (!data?.groups) return Array(12).fill(0);

    const totals = Array(12).fill(0);

    data.groups.forEach(g => {
      (g.categories ?? []).forEach(cat => {
        (cat.values ?? []).forEach(v => {
          totals[v.month - 1] += v.amount;
        });
      });
    });

    return totals;
  });

  groupMonthTotals = computed(() => {
    const data = this.dataSignal();
    if (!data?.groups) return {};

    const result: Record<string, number[]> = {};

    data.groups.forEach(g => {
      const totals = Array(12).fill(0);

      (g.categories ?? []).forEach(cat => {
        (cat.values ?? []).forEach(v => {
          totals[v.month - 1] += v.amount;
        });
      });

      result[g.name] = totals;
    });

    return result;
  });


  netFlow = computed(() => {
    return this.monthTotals();
  });

  accumulated = computed(() => {
    const flow = this.monthTotals();
    const result: number[] = [];

    let acc = 0;

    flow.forEach(v => {
      acc += v;
      result.push(acc);
    });

    return result;
  });

  // permite mover el scroll horizontal para centrar el mes actual al cargar la tabla
  scrollToCurrentMonth() {
    const wrapper = this.tableWrapper.nativeElement;
    this.currentMonthIndex = new Date().getMonth(); // 0 = Enero
    const firstCell = wrapper.querySelector('th:nth-child(2)') as HTMLElement;
    if (!firstCell) return;
    const columnWidth = firstCell.offsetWidth;
    // +1 porque la primera columna es "Summary"
    const scrollPosition = columnWidth * (this.currentMonthIndex );

    wrapper.scrollTo({
      left: scrollPosition,
      behavior: 'smooth'
    });
  }

}
