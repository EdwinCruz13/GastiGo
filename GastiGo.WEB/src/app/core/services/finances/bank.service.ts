//crear un servicio para manejar las finanzas del banco
//que permita ver el listado de bancos existentes

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Bank, BankRequestDTO } from '@core/models/finances/bank.model';
import { environment } from '@env/environment';
import { ApiResponse } from '@core/models/common/api-response.model';

/**
 * Servicio para manejar los bancos existentes
 */
@Injectable({
  providedIn: 'root'
})
export class BankService {
  // URL de la API para los bancos
  private apiUrl = `${environment.apiUrl}/finance/banks`; // URL de la API para los bancos

  // Inyectar HttpClient para realizar solicitudes HTTP
  constructor(private http: HttpClient) { }

  // Método para crear un nuevo banco, recibe un objeto BankRequestDTO con los datos del banco a crear.
  create(bank: BankRequestDTO): Observable<ApiResponse<object>> {
    return this.http.post<ApiResponse<object>>(this.apiUrl, bank);
  }

  // Método para actualizar un banco existente, recibe el ID del banco a actualizar y un objeto BankRequestDTO con los nuevos datos del banco.
  update(id: string, bank: BankRequestDTO): Observable<ApiResponse<object>>
    {
      return this.http.put<ApiResponse<object>>(
        `${this.apiUrl}/${id}`,
        bank
      );
    }

  // Método para obtener el listado de bancos existentes
  getBanks(): Observable<ApiResponse<Bank[]>> {
    return this.http.get<ApiResponse<Bank[]>>(this.apiUrl);
  }

  // Método para obtener un banco por su ID
  getBankById(bankId: string): Observable<ApiResponse<Bank>> {
    return this.http.get<ApiResponse<Bank>>(`${this.apiUrl}/${bankId}`);
  }
}
