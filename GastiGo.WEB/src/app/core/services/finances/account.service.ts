//crear un servicio para manejar las finanzas de cuenta, que permita ver el listado de tipos de cuenta existentes
//que permita ver el listado de tipos de cuenta existentes

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '@env/environment';
import { ApiResponse } from '@core/models/common/api-response.model';
import { Account, AccountRequestDTO } from '@core/models/finances/account.model';

/**
 * Servicio para manejar las cuentas existentes
 */
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  // URL de la API para los tipos de cuenta
  private apiUrl = `${environment.apiUrl}/finance/accounts`; // URL de la API para los tipos de cuenta

  // Inyectar HttpClient para realizar solicitudes HTTP
  constructor(private http: HttpClient) { }

  /// Método para crear una nueva cuenta enviando los datos de la cuenta al endpoint de la API.
  create(account: AccountRequestDTO): Observable<ApiResponse<object>> {
      return this.http.post<ApiResponse<object>>(
        `${this.apiUrl}`,
        account
      );
  }

  // Método para obtener el listado de tipos de cuenta existentes
  getAccounts(userID: string): Observable<ApiResponse<Account[]>> {
    return this.http.get<ApiResponse<Account[]>>(`${this.apiUrl}?userId=${userID}`);
  }

  // Método para obtener un tipo de cuenta por su ID
  getAccountById(accountId: string): Observable<ApiResponse<Account>> {
    return this.http.get<ApiResponse<Account>>(`${this.apiUrl}/${accountId}`);
  }
}
