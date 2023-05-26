import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginData } from './login.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = "";
  password: string = "";

  constructor(private http: HttpClient) { }

  login() {
    const loginData: LoginData = {
        EmployeeEmail: this.email,
        EmployeePasswordHash: this.password   
    };

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        //'Access-Control-Allow-Origin': '*',
        //'Access-Control-Allow-Credentials': 'true',
        //'Access-Control-Allow-Headers': 'Content-Type',
        //'Access-Control-Allow-Methods': 'GET,PUT,POST,DELETE',
      })
    };

    this.http.post('https://localhost:7126/bugtracker/account/login', loginData, httpOptions).subscribe(
      (response) => {
        // Obsługa sukcesu logowania
        console.log('Zalogowano', response);
        const token = typeof response === 'string' ? response : response as string;
        httpOptions.headers = httpOptions.headers.set('Authorization', `Bearer ${token}`);
      },
      (error) => {
        // Obsługa błędu logowania
        console.error('Błąd logowania', error.error);
      }
    );
  }
}
