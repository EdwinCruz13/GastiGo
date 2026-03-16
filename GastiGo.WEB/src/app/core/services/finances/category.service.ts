import { Injectable } from '@angular/core';
import { Category, CategoryRequestDTO } from '@core/models/finances/category.model';

import { environment } from '@env/environment';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '@core/models/common/api-response.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  // esta ruta es la url del proyecto
  private api = environment.apiUrl;

  constructor(private http: HttpClient) {}

  // obtiene el árbol de categorías para un usuario específico, utilizando su ID.
  // Retorna un observable con la respuesta de la API que contiene un array de categorías.
  getTree(userId: string) {
    return this.http.get<ApiResponse<Category[]>>(`${this.api}/finance/categories?userId=${userId}`);
  }

  // crea una nueva categoría enviando los datos de la categoría al endpoint de la API.
  // Retorna un observable con la respuesta de la API que contiene la categoría creada.
  create(category: CategoryRequestDTO): Observable<ApiResponse<object>> {
    return this.http.post<ApiResponse<object>>(
      `${this.api}/finance/categories`,
      category
    );
  }

  // actualiza una categoría existente enviando los datos de la categoría y su ID al endpoint de la API.
  //retorna un observable con la respuesta de la API que contiene la categoría actualizada.
  update(id: string, category: CategoryRequestDTO): Observable<ApiResponse<object>>
  {
    return this.http.put<ApiResponse<object>>(
      `${this.api}/finance/categories/${id}`,
      category
    );
  }

  // elimina una categoría existente enviando su ID al endpoint de la API.
  // Retorna un observable con la respuesta de la API que contiene un objeto vacío.
  delete(id: string): Observable<ApiResponse<object>>
  {
    return this.http.delete<ApiResponse<object>>(
      `${this.api}/finance/categories/${id}`
    );
  }

}
