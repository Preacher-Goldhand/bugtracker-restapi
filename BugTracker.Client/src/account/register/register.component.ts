// importujemy moduł HttpClient
import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RegisterData } from './register.model';

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

  constructor(private http: HttpClient) { } // wstrzykujemy HttpClient w konstruktorze

  registerUser() {
    // Przesyłamy dane do serwera
    this.http.post('https://localhost:7126/bugtracker/account/register', this.registerData)
      .subscribe(
        response => {
          console.log('Sukces:', response);
          // Tutaj możesz obsłużyć odpowiedź serwera po udanej rejestracji
        },
        error => {
          console.log('Błąd:', error);
          // Tutaj możesz obsłużyć błąd, jeśli rejestracja nie powiodła się
        }
      );
  }

  passwordMismatch(): boolean {
    return this.registerData.EmployeePasswordHash !== this.registerData.ConfirmEmployeePasswordHash;
  }
}
