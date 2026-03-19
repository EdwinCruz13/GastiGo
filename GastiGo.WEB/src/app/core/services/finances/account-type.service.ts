//crear un servicio para manejar las finanzas del tipo de cuenta, que permita ver el listado de tipos de cuenta existentes
//que permita ver el listado de tipos de cuenta existentes

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AccountType } from '@core/models/finances/accountType.model';
import { environment } from '@env/environment';
import { ApiResponse } from '@core/models/common/api-response.model';

/**
 * Servicio para manejar los tipos de cuenta existentes
 */
@Injectable({
  providedIn: 'root'
})
export class AccountTypeService {
  // URL de la API para los tipos de cuenta
  private apiUrl = `${environment.apiUrl}/finance/accountypes`; // URL de la API para los tipos de cuenta

  // Inyectar HttpClient para realizar solicitudes HTTP
  constructor(private http: HttpClient) { }

  // Método para obtener el listado de tipos de cuenta existentes
  getAccountTypes(): Observable<ApiResponse<AccountType[]>> {
    return this.http.get<ApiResponse<AccountType[]>>(this.apiUrl);
  }

  // Método para obtener un tipo de cuenta por su ID
  getTypeAccountById(typeId: string): Observable<ApiResponse<AccountType>> {
    return this.http.get<ApiResponse<AccountType>>(`${this.apiUrl}/${typeId}`);
  }
}
