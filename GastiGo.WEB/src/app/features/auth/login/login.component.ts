import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { LoginRequest } from '@core/models/auth/login-request.model';
import { AuthService } from '@core/services/auth/auth.service';

/**
 * Componente de inicio de sesión que permite a los usuarios ingresar su correo electrónico y contraseña para autenticarse en la aplicación.
 * Utiliza Reactive Forms para manejar el formulario de inicio de sesión y aplicar validaciones a los campos de correo electrónico y contraseña.
 * El formulario requiere que el correo electrónico sea obligatorio y que la contraseña tenga al menos 6 caracteres.
 */
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  imports: [CommonModule, ReactiveFormsModule, RouterLink, RouterLinkActive],
  standalone: true
})

export class LoginComponent {
  //inyecta el servicio de auth service para conectarse
  authService = inject(AuthService);

  //inyecta el router para rediccionar
  router = inject(Router);

  //inyecta el servicio FormBuilder para crear el formulario de inicio de sesión
  FormBuilder = inject(FormBuilder);

  //crea el formulario de inicio de sesión con validaciones
  loginForm = this.FormBuilder.group({
     email: ['', [Validators.required, Validators.nullValidator]], //campo de correo electrónico con validación de requerido
     password: ['', [Validators.required, Validators.nullValidator, Validators.minLength(6)]] //campo de contraseña con validación de requerido y longitud mínima de 6 caracteres
  });

  apiErrors: string[] = [];

  /**
   * Maneja el evento de envío del formulario de inicio de sesión. Verifica si el formulario es válido y, si lo es, obtiene los valores de correo electrónico y contraseña, y los imprime en la consola. Si el formulario no es válido, imprime un mensaje indicando que el formulario no es válido.
   */
  onSubmit() {

    if (!this.loginForm.valid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    //crea un nuevo objecto de tipo LoginRequesst
    const request = this.loginForm.value as LoginRequest;


    //consumimos el webservices
    this.authService.login(request).subscribe({
      next: (response) => {
        //guarda el token en el localstorage
        const token = response.data?.accessToken;
        if(token)
          localStorage.setItem("token", token);

        //resetea el formulario
        this.loginForm.reset();

        //inicializa el nombre del usuario en el servicio de autenticación
        this.authService.initUser();

        //redireccionar
        this.router.navigate(['/dashboard']);

      },

      //si existe problemas, entonces imprimir mensaje de error en pantalla
      error: (err) => {
        this.apiErrors = err.error.errors ?? ["Error desconocido"];
        console.log(err)

      }
    });
  }
}
