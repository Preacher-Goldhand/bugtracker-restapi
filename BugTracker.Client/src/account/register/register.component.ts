import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RegisterData } from './register.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerData: RegisterData = {
    FirstName: '',
    LastName: '',
    Department: '',
    EmployeeEmail: '',
    EmployeePasswordHash: '',
    ConfirmEmployeePasswordHash: '',
    EmployeePhoneNumber: '',
    EmployeeStatus: ''
  };

  constructor(private http: HttpClient, private router: Router) { }

  registerUser() {
    this.http.post('https://localhost:7126/bugtracker/account/register', this.registerData)
      .subscribe(
        response => {
          console.log('Sucess:', response);
          this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
            this.router.navigate(['/login']);
          });
        },
        error => {
          console.log('Fail:', error);
        }
      );
  }

  passwordMismatch(): boolean {
    return this.registerData.EmployeePasswordHash !== this.registerData.ConfirmEmployeePasswordHash;
  }
}
