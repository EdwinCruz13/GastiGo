import { Injectable } from '@angular/core';
import { Nature } from '@core/models/finances/nature.model';

import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '@core/models/common/api-response.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NatureService {
  // esta ruta es la url del proyecto
  private api = environment.apiUrl;

  // inyectamos el servicio HttpClient para hacer peticiones HTTP
  constructor(private http: HttpClient) {}

  //obtiene todas naturalezas de la base de datos
  getAll() : Observable<ApiResponse<Nature[]>> {
    return this.http.get<ApiResponse<Nature[]>>(`${this.api}/finance/natures`);
  }

}
