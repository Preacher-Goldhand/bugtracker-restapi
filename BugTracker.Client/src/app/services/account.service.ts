import { Injectable } from '@angular/core';
import { UserDetails } from '../model/user-details';
import jwt_decode from "jwt-decode";
import { BehaviorSubject, Observable } from 'rxjs';
import { EmployeeData } from '../../dashboard/models/employee-model';


@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private _userLogin = new BehaviorSubject<boolean>(false);
  private _userLogin$ = this._userLogin.asObservable();
  private _availableHoursSubject = new BehaviorSubject<number>(0);
  _availableHours$ = this._availableHoursSubject.asObservable();
  private userDetails: UserDetails;
  private filteredEmployees: EmployeeData[] = [];

  constructor() {
    this.userDetails = {} as UserDetails;
  }

  public setUserLogin(value: boolean): void {
    this._userLogin.next(value);
  }

  public getUserLogin(): Observable<boolean> {
    return this._userLogin$;
  }

  public setUserDetails(token: string): void {
    const jwt = jwt_decode<any>(token);
    this.userDetails = {
      id: jwt["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"],
      name: jwt["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
      role: jwt["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
      //availableHours: jwt["http://schemas.bugtracker.com/claims/availableHours"]
    };
    this._userLogin.next(true);
  }

  public getUserDetails(): UserDetails {
    return this.userDetails;
  }

  public updateAvailableHours(hours: number): void {
    this.userDetails.availableHours = hours;
    this._availableHoursSubject.next(hours);
  }

  public setFilteredEmployees(employees: EmployeeData[]): void {
    this.filteredEmployees = employees;
  }

  public updateAvailableHoursInView(updatedHours: number, employeeId: number): void {
    const index = this.filteredEmployees.findIndex(employee => employee.id === employeeId);
    if (index !== -1) {
      this.filteredEmployees[index].availableHours = updatedHours;
    }
  }
}
