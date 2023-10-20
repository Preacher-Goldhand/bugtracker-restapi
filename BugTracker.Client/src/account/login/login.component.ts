import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginData } from './login.model';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = "";
  password: string = "";

  constructor(private http: HttpClient, private router: Router, private accountService: AccountService) {
    this.accountService.setUserLogin(false);
  }

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

    this.http.post('https://localhost:7126/bugtracker/account/login', loginData, { responseType: 'text' }).subscribe(
      (response) => {
        sessionStorage.setItem('jwt', response);
        this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
          this.router.navigate(['dashboard']);
        });
        this.accountService.setUserDetails(response);
        this.accountService.setUserLogin(true);
      },
      (error) => {
        console.error('Fail:', error.error);
      }
    );
  }
}
