import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class EmployeeHoursComponent {
  private _availableHoursSource = new BehaviorSubject<number | undefined>(undefined);
  _availableHours$ = this._availableHoursSource.asObservable();

  updateAvailableHours(updatedHours: number): void {
    this._availableHoursSource.next(updatedHours);
  }
}
