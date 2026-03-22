//crear un servicio para manejar las transacciones
//que permita ver el listado de transacciones existentes

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '@env/environment';
import { ApiResponse } from '@core/models/common/api-response.model';
import { Transaction } from '@core/models/finances/transaction.model';

/**
 * Servicio para manejar las transacciones existentes
 */
@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  // URL de la API para las transacciones
  private apiUrl = `${environment.apiUrl}/finance/transactions`; // URL de la API para las transacciones

  // Inyectar HttpClient para realizar solicitudes HTTP
  constructor(private http: HttpClient) { }

  // Método para obtener el listado de transacciones existentes
  getTransactions(userID: string): Observable<ApiResponse<Transaction[]>> {
    return this.http.get<ApiResponse<Transaction[]>>(`${this.apiUrl}?userID=${userID}`);
  }

  // Método para obtener una transacción por su ID
  getTransactionById(transactionId: string): Observable<ApiResponse<Transaction>> {
    return this.http.get<ApiResponse<Transaction>>(`${this.apiUrl}/${transactionId}`);
  }
}
