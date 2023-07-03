import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { BoardData } from '../../models/board.model';
import { PagedResult } from '../../models/paged-result.model';

@Component({
  selector: 'app-boardservice',
  templateUrl: './boardservice.component.html',
  styleUrls: ['./boardservice.component.css']
})
export class BoardServiceComponent {
  boards: BoardData[] = [];
  searchPhrase: string = '';
  pageNumber: number = 1;
  pageSize: number = 5;
  totalPages: number = 0;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getData();
  }

  getData() {
    let url = `https://localhost:7126/bugtracker/board?pageSize=${this.pageSize}&pageNumber=${this.pageNumber}`;
    if (this.searchPhrase) {
      url += `&searchPhrase=${this.searchPhrase}`;
    }

    this.http.get<PagedResult<BoardData>>(url)
      .subscribe((result: PagedResult<BoardData>) => {
        this.boards = result.items;
        this.totalPages = result.totalPages;
      });
  }

  getBoards() {
    this.pageNumber = 1;
    this.getData();
  }

  search() {
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
    this.pageSize = +pageSize;
    this.getData();
  }
}
