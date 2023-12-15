import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AccountService } from '../../../app/services/account.service';
import { EmployeeData } from '../../models/employee-model';
import { PagedResult } from '../../models/paged-result.model';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent implements OnInit {
  employeeDetails: EmployeeData[] = [];
  searchPhrase: string = '';
  filteredEmployees: EmployeeData[] = [];
  noResultsMessage: string = '';
  pageSize: number = 5;
  pageNumber: number = 1;
  totalPages: number = 0;
  private _employeeId: number | undefined;
  private availableHoursSubscription!: Subscription;
  isUser: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private datePipe: DatePipe,
    private router: Router,
    private accountService: AccountService
  ) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this._employeeId = params['id'];
      this.getData();
    });
    const userRole = this.accountService.getUserDetails().role;
    this.isUser = userRole === 'User';

    if (this.isUser) {
      alert('Welcome! You are in the User role.');
    }

  }

  getData(): void {
    const url = `https://localhost:7126/bugtracker/employee?pageSize=${this.pageSize}&pageNumber=${this.pageNumber}`;
    this.http.get<PagedResult<EmployeeData>>(url)
      .subscribe((result: PagedResult<EmployeeData>) => {
        this.employeeDetails = result.items;
        this.totalPages = result.totalPages;
        this.accountService.setFilteredEmployees(this.employeeDetails);
      });
  }

  searchEmployee() {
    console.log('Search phrase:', this.searchPhrase);

    if (this.searchPhrase === '') {
      this.filteredEmployees = this.employeeDetails;
      this.noResultsMessage = '';
    } else {
      this.filteredEmployees = this.employeeDetails.filter(employee =>
        employee.firstName.toLowerCase().includes(this.searchPhrase.toLowerCase()) ||
        employee.lastName.toLowerCase().includes(this.searchPhrase.toLowerCase()) ||
        employee.department.toLowerCase().includes(this.searchPhrase.toLowerCase()) ||
        employee.employeeEmail.toLowerCase().includes(this.searchPhrase.toLowerCase()) ||
        (employee.employeePhoneNumber && employee.employeePhoneNumber.toString().includes(this.searchPhrase)) ||
        employee.employeeStatus.toLowerCase().includes(this.searchPhrase.toLowerCase())
      );

      console.log('Filtered employees:', this.filteredEmployees);

      this.noResultsMessage = this.filteredEmployees.length === 0 ? 'No results found for the entered search phrase' : '';
    }
  }

  resetSearch() {
    this.searchPhrase = '';
    this.pageNumber = 1;
  }

  editEmployee(employeeId: number) {
    this.router.navigate(['/employee-edit', employeeId]);
  }

  removeEmployee(employeeId: number) {
    const url = `https://localhost:7126/bugtracker/employee/${employeeId}`;
    const confirmDelete = confirm('Are you sure you want to delete this employee');

    if (confirmDelete) {
      this.http.delete<EmployeeData>(url)
        .subscribe(() => {
          this.router.navigate(['/employees']);
          this.getData();
        });
    } else {
      this.router.navigate(['/employees']);
    }
  }

  getRoleName(roleId: number): string {
    switch (roleId) {
      case 1:
        return 'User';
      case 2:
        return 'Manager';
      case 3:
        return 'Admin';
      default:
        return 'Unknown Role';
    }
  }

  updatePage() {
    this.pageNumber = 1;
    this.getData();
  }

  previousPage() {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.getData();
    }
  }

  nextPage() {
    if (this.pageNumber < this.totalPages) {
      this.pageNumber++;
      this.getData();
    }
  }

  onPageSizeChange(event: Event) {
    const pageSize = (event.target as HTMLSelectElement).value;
    this.pageSize = + pageSize;
    this.pageNumber = 1;
    this.getData();
  }

  formatDateString(date: Date): string {
    const options: Intl.DateTimeFormatOptions = { year: 'numeric', month: '2-digit', day: '2-digit' };
    return this.datePipe.transform(date, 'shortDate')!;
  }
}
