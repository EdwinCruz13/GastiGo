//crear un servicio para manejar las finanzas del banco
//que permita ver el listado de bancos existentes

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Currency } from '@core/models/finances/currency.model';
import { environment } from '@env/environment';
import { ApiResponse } from '@core/models/common/api-response.model';

/**
 * Servicio para manejar las monedas existentes
 */
@Injectable({
  providedIn: 'root'
})
export class CurrencyService {
  // URL de la API para las monedas
  private apiUrl = `${environment.apiUrl}/finance/currencies`; // URL de la API para las monedas

  // Inyectar HttpClient para realizar solicitudes HTTP
  constructor(private http: HttpClient) { }


  // Método para obtener el listado de monedas existentes
  getCurrencies(): Observable<ApiResponse<Currency[]>> {
    return this.http.get<ApiResponse<Currency[]>>(this.apiUrl);
  }

  // Método para obtener una moneda por su ID
  getCurrencyById(currencyId: string): Observable<ApiResponse<Currency>> {
    return this.http.get<ApiResponse<Currency>>(`${this.apiUrl}/${currencyId}`);
  }
}
