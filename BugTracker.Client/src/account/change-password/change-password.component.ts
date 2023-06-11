import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ChangePasswordData } from './change-password.model';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent {
  changePasswordData: ChangePasswordData = {
    EmployeeEmail: '',
    CurrentPasswordHash: '',
    NewPasswordHash: ''
  };

  constructor(private http: HttpClient, private router: Router) { }

  changePassword() {
    this.http.post('https://localhost:7126/bugtracker/account/changePassword', this.changePasswordData)
      .subscribe(
        response => {
          console.log('Sukces:', response);
          this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
            this.router.navigate(['/login']);
          });
        },
        error => {
          console.log('Błąd:', error);
        }
      );
  }
}
