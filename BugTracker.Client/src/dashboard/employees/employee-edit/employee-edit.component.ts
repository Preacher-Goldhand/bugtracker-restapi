import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeHoursComponent } from '../../employee-hours/employee-hours.component';
import { EmployeeData } from '../../models/employee-model';

@Component({
  selector: 'app-employee-edit',
  templateUrl: './employee-edit.component.html',
  styleUrls: ['./employee-edit.component.css']
})
export class EmployeeEditComponent implements OnInit {
  employee: EmployeeData = {
    id: 0,
    firstName: '',
    lastName: '',
    department: '',
    employeeEmail: '',
    employeePhoneNumber: '',
    availableHours: 0,
    employeeStatus: '',
    roleId: 0
  };

  private _employeeId: number | undefined;

  constructor(private route: ActivatedRoute,
    private http: HttpClient,
    private router: Router,
    private availableHoursService: EmployeeHoursComponent) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this._employeeId = params['id'];

      if (this._employeeId !== undefined) {
        this.http.get<EmployeeData>(`https://localhost:7126/bugtracker/employee/${this._employeeId}`)
          .subscribe((data) => {
            this.employee = data;
            this.availableHoursService.updateAvailableHours(data.availableHours);
            console.log('Updated Hours:', data.availableHours);
          });
      }
    });
  }

  editEmployee(): void {
    if (this._employeeId !== undefined) {
      this.http.put(`https://localhost:7126/bugtracker/employee/${this._employeeId}`, this.employee)
        .subscribe({
          next: value => {
            this.router.navigate(['/employees']);
          },
          error: err => {
            console.error('Error editing employee:', err);
          }
        });
    } else {
      console.error('_employeeId is undefined. Cannot edit employee.');
    }
  }

  cancelEditEmployee(): void {
    this.router.navigate(['/employees']);
  }
}
