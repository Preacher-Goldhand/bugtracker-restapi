import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-boardservice',
  templateUrl: './boardservice.component.html',
  styleUrls: ['./boardservice.component.css']
})
export class BoardServiceComponent {
  private baseUrl = 'https://localhost:7126/bugtracker/board';

  constructor(private http: HttpClient) { }

  getBugBoard(pageSize: number, pageNumber: number) {
    const url = `${this.baseUrl}?pageSize=${pageSize}&pageNumber=${pageNumber}`;
    return this.http.get<any[]>(url);
  }
}
