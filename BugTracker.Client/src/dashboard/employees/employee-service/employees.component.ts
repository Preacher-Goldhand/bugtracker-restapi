import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeData } from '../../models/employee-model';
import { PagedResult } from '../../models/paged-result.model';


@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent {

  employeeDetails: EmployeeData[] = [];

  searchPhrase: string = '';
  filteredEmployees: EmployeeData[] = [];
  noResultsMessage: string = '';
  pageSize: number = 5;
  pageNumber: number = 1;
  totalPages: number = 0;

  private _employeeId: number | undefined;

  constructor(private route: ActivatedRoute,
    private http: HttpClient,
    private datePipe: DatePipe,
    private router: Router) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this._employeeId = params['id'];
      this.getData();
    });
  }

  getData(): void {
    const url = `https://localhost:7126/bugtracker/employee?pageSize=${this.pageSize}&pageNumber=${this.pageNumber}`;
    this.http.get<PagedResult<EmployeeData>>(url)
      .subscribe((result: PagedResult<EmployeeData>) => {
        this.employeeDetails = result.items;
        this.totalPages = result.totalPages;
      });
  }

  searchEmployee() {
    if (this.searchPhrase === '') {
      this.filteredEmployees = [];
      this.noResultsMessage = '';
      return;
    }

    this.filteredEmployees = this.employeeDetails.filter(employee =>
      employee.firstName.toLowerCase().includes(this.searchPhrase.toLowerCase()) ||
      employee.lastName.toLowerCase().includes(this.searchPhrase.toLowerCase()) ||
      employee.department.toLowerCase().includes(this.searchPhrase.toLowerCase()) ||
      employee.employeeEmail.toLowerCase().includes(this.searchPhrase.toLowerCase()) ||
      employee.employeePhoneNumber.includes(this.searchPhrase) ||
      employee.employeeStatus.toLowerCase().includes(this.searchPhrase.toLowerCase())
    );

    if (this.filteredEmployees.length === 0) {
      this.noResultsMessage = 'No results found for the entered search phrase';
    } else {
      this.noResultsMessage = '';
    }
    this.updatePagedEmployees();
  }

  editEmployee(employeeId: number) {
    this.router.navigate(['/employee-edit', employeeId]);
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
    this.searchEmployee();
  }

  previousPage() {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.searchEmployee();
    }
  }

  nextPage() {
    if (this.pageNumber < this.totalPages) {
      this.pageNumber++;
      this.searchEmployee();
    }
  }

  updatePagedEmployees() {
    const startIndex = (this.pageNumber - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.filteredEmployees = this.filteredEmployees.slice(startIndex, endIndex);
  }

  formatDateString(date: Date): string {
    const options: Intl.DateTimeFormatOptions = { year: 'numeric', month: '2-digit', day: '2-digit' };
    return this.datePipe.transform(date, 'shortDate')!;
  }
}
