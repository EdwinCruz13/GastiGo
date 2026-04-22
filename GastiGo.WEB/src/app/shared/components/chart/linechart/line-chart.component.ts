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
  selector: 'app-line-chart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './line-chart.component.html',
  styleUrls: ['./line-chart.component.css']
})
export class LineChartComponent implements OnChanges, AfterViewInit {

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
      type: 'line',
      data: this.buildData(),
      options: this.getOptions()
    });
  }

  // =========================
  // Actualizar gráfico
  // =========================
  private updateChart() {
    this.chart.data = this.buildData();
    this.chart.update();
  }

  // =========================
  // Data builder
  // =========================
  private buildData(): ChartConfiguration['data'] {
    return {
      labels: this.labels,
      datasets: this.datasets.map(d => ({
        label: d.label,
        data: d.data,
        borderColor: d.color,
        backgroundColor: d.color,

        tension: 0,
        fill: false,
        borderWidth: 2,
        pointRadius: 3,
        pointHoverRadius: 5,
        pointBackgroundColor: d.color
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
        }
      },
      scales: {
        x: {
          ticks: { color: '#9ca3af' }
        },
        y: {
          ticks: { color: '#9ca3af' }
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
