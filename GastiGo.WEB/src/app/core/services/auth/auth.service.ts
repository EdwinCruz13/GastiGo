import { HttpClient } from '@angular/common/http';
import { computed, Injectable, signal } from '@angular/core';
import { jwtDecode } from 'jwt-decode';

import { environment } from 'src/environments/environment';
import { LoginRequest } from '@core/models/auth/login-request.model';
import { LoginResponse } from '@core/models/auth/login-response.model';
import { ApiResponse } from '@core/models/common/api-response.model';
import { JwtPayload } from '@core/models/common/jwtpayload.model';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  // esta ruta es la url del proyecto
  private api = environment.apiUrl;

  // señal para almacenar el nombre del usuario autenticado
  userName = signal<string | null>(null);
  userId = signal<string | null>(null);

  // señal computada para obtener la inicial del avatar a partir del nombre del usuario
  avatarInitial = computed(() => {
    const name = this.userName();
    return name ? name.trim().charAt(0).toUpperCase() : '?';
  });


  // instanciar el modulo httpCLiente
  constructor(private http: HttpClient) {
    this.initUser();// inicializa el nombre del usuario al crear el servicio
  }

  /**
   * permite logearse
   * @param data
   * @returns
   */
  login(data: LoginRequest){
    return this.http.post<ApiResponse<LoginResponse>>(
      `${this.api}/auth/login`,
      data
    );
  }

  /**
   * obtiene el token guardado en el localstorage
   * @returns
   */
  getToken(): string | null {
   return localStorage.getItem("token");
  }

  //verifica si ya esta autenticado
  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  //cerrar sesión
  logout(){
    localStorage.removeItem("token");
  }

  // inicializa el nombre del usuario y el ID del usuario a partir del
  // token almacenado en el localstorage
  initUser(): void {
    const token = localStorage.getItem('token');
    if (!token) return;
    const decoded = jwtDecode<JwtPayload>(token);
    this.userName.set(decoded.name);
    this.userId.set(decoded.sub);
  }

}
