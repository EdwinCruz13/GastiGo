import {
  Component,
  Input,
  OnChanges,
  SimpleChanges,
  AfterViewInit,
  ViewChild,
  ElementRef
} from '@angular/core';

import { CommonModule } from '@angular/common';
import {
  Chart,
  ChartConfiguration,
  registerables
} from 'chart.js';

Chart.register(...registerables);

@Component({
  selector: 'app-bar-chart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './bar-chart.component.html',
  styleUrls: ['./bar-chart.component.css']
})
export class BarChartComponent implements OnChanges, AfterViewInit {

  @Input({ required: true }) labels: string[] = [];
  @Input({ required: true }) datasets: {
    label: string;
    data: number[];
    color: string;
  }[] = [];

  @ViewChild('canvas') canvas!: ElementRef<HTMLCanvasElement>;

  chart!: Chart;

  ngAfterViewInit(): void {
    this.createChart();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (this.chart) {
      this.updateChart();
    }
  }

  // =========================
  // Crear gráfico
  // =========================
  private createChart() {
    if (!this.canvas) return;

    this.chart = new Chart(this.canvas.nativeElement, {
      type: 'bar',
      data: this.buildData(),
      options: this.getOptions()
    });
  }

  // =========================
  // Actualizar
  // =========================
  private updateChart() {
    this.chart.data = this.buildData();
    this.chart.update();
  }

  // =========================
  // Data
  // =========================
  private buildData(): ChartConfiguration['data'] {
    return {
      labels: this.labels,
      datasets: this.datasets.map(d => ({
        label: d.label,
        data: d.data,
        backgroundColor: this.hexToRgba(d.color, 0.6),
        borderColor: d.color,
        borderWidth: 1,
        borderRadius: 6, // 🔥 look moderno
        barThickness: 24
      }))
    };
  }

  // =========================
  // Options
  // =========================
  private getOptions(): ChartConfiguration['options'] {
    return {
      responsive: true,
      maintainAspectRatio: false,

      plugins: {
        legend: {
          labels: {
            color: '#e5e7eb'
          }
        },
        tooltip: {
          callbacks: {
            label: (ctx: any) => {
              return `${ctx.dataset.label}: C$ ${ctx.raw.toLocaleString()}`;
            }
          }
        }
      },

      scales: {
        x: {
          ticks: { color: '#9ca3af' },
          grid: { display: false }
        },
        y: {
          ticks: { color: '#9ca3af' },
          grid: {
            color: 'rgba(255,255,255,0.05)'
          }
        }
      }
    };
  }

  // =========================
  // Helper color
  // =========================
  private hexToRgba(hex: string, alpha: number) {
    const r = parseInt(hex.substring(1, 3), 16);
    const g = parseInt(hex.substring(3, 5), 16);
    const b = parseInt(hex.substring(5, 7), 16);
    return `rgba(${r},${g},${b},${alpha})`;
  }
}
