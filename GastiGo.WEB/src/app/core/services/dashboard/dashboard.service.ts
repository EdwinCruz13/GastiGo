import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DashboardYear } from '@core/models/dashboard/dashboard.model';
import { Observable } from 'rxjs';

import { environment } from '@env/environment';
import { ApiResponse } from '@core/models/common/api-response.model';

/**
 * servicio para manejar el dashboard del año, que permita ver el listado de ingresos y gastos por año
 */
@Injectable({ providedIn: 'root' })
export class DashboardService {

  private apiUrl = `${environment.apiUrl}/dashboard`; // URL de la API para las transacciones

  // Inyectar HttpClient para realizar solicitudes HTTP
  constructor(private http: HttpClient) {}

  // Método para obtener el dashboard del año
  getIncomeAndExpensesByUserAndYear(userId: string, year: number): Observable<ApiResponse<DashboardYear>> {
    return this.http.get<ApiResponse<DashboardYear>>(
      `${this.apiUrl}/getIncomeAndExpensesByYear?userId=${userId}&year=${year}`
    );
  }

  getSavingsByUserAndYear(userId: string, year: number): Observable<ApiResponse<DashboardYear>> {
    return this.http.get<ApiResponse<DashboardYear>>(
      `${this.apiUrl}/GetSavingsByUserAndYear?userId=${userId}&year=${year}`
    );
  }


  getInvestmentsByUserAndYear(userId: string, year: number): Observable<ApiResponse<DashboardYear>> {
    return this.http.get<ApiResponse<DashboardYear>>(
      `${this.apiUrl}/GetInvestmentByUserAndYear?userId=${userId}&year=${year}`
    );
  }
}
