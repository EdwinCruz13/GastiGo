//crear un servicio para manejar los tipos de transacciones
//que permita ver el listado de Tipo de transacciones existentes

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TransactionType } from '@core/models/finances/transactionType.model';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '@core/models/common/api-response.model';

/**
 * Servicio para manejar los tipos de transacciones existentes
 */
@Injectable({
  providedIn: 'root'
})
export class TransactionTypeService {
  // URL de la API para los tipos de transacciones
  private apiUrl = `${environment.apiUrl}/finance/transactiontypes`; // URL de la API para los tipos de transacciones

  // Inyectar HttpClient para realizar solicitudes HTTP
  constructor(private http: HttpClient) { }

  // Método para obtener el listado de tipos de transacciones existentes
  getTransactionTypes(): Observable<ApiResponse<TransactionType[]>> {
    return this.http.get<ApiResponse<TransactionType[]>>(this.apiUrl);
  }

  // Método para obtener un tipo de transacción por su ID
  getTransactionTypeById(transactionTypeId: string): Observable<ApiResponse<TransactionType>> {
    return this.http.get<ApiResponse<TransactionType>>(`${this.apiUrl}/${transactionTypeId}`);
  }
}
