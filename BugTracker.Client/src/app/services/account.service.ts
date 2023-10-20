import { Injectable } from '@angular/core';
import { UserDetails } from '../model/user-details';
import jwt_decode from "jwt-decode";
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private _userLogin = new BehaviorSubject<boolean>(false);
  private _userLogin$ = this._userLogin.asObservable();

  private userDetails: UserDetails;

  constructor() { 
    this.userDetails = { } as UserDetails;
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
        role: jwt["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
    };
  }

  public getUserDetails(): UserDetails {
    return this.userDetails;
  }
}
