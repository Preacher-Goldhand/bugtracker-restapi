import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginData } from './login.model';
import { JwtHelperService } from '@auth0/angular-jwt';

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
      })
    };

    this.http.post('https://localhost:7126/bugtracker/account/login', loginData).subscribe(
      (response) => {
        // Obsługa sukcesu logowania
        console.log('Success:', response);
        const encodedToken = typeof response === 'string' ? response : response as string;
        const helper = new JwtHelperService();
        const decodedToken = helper.decodeToken(encodedToken);
        console.log('Decoded token:', decodedToken);
        return decodedToken;
      },
      (error) => {
        // Obsługa błędu logowania
        console.error('Fail:', error.error);
      }
    );
  }
}
