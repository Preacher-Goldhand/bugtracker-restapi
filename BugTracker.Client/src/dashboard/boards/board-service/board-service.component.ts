import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, NavigationExtras, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { BoardData } from '../../models/board.model';
import { PagedResult } from '../../models/paged-result.model';
import { DetailedBoardData } from '../../models/detailed-board.model';
import { UpdateBoardData } from '../../models/update-board.model';

@Component({
  selector: 'app-boardservice',
  templateUrl: './board-service.component.html',
  styleUrls: ['./board-service.component.css']
})
export class BoardServiceComponent implements OnInit {
  boards: BoardData[] = [];
  searchPhrase: string = '';
  pageNumber: number = 1;
  pageSize: number = 5;
  totalPages: number = 0;
  noResultsMessage: string = '';

  constructor(private route: ActivatedRoute, private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      const boardId = params['id'];
      if (boardId && confirm('Are you sure you want to delete this board?')) {
        this.removeBoard(boardId);
      }
    });
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

        if (this.boards.length === 0 && this.searchPhrase) {
          this.noResultsMessage = 'No results found for the entered search phrase';
        } else {
          this.noResultsMessage = '';
        }
      });
  }

  searchBoard() {
    this.pageNumber = 1;
    this.getData();
  }

  addBoard() {
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/add-board']);
    });
  }

  showBoardDetails(board: BoardData) {
    const url = `https://localhost:7126/bugtracker/board/${board.id}`;

    this.http.get<DetailedBoardData>(url)
      .subscribe(() => {
        this.router.navigate(['/board-details', board.id]);
      });
  }

  editBoard(boardId: number) {
    const url = `https://localhost:7126/bugtracker/board/${boardId}`;
    this.router.navigate(['/edit-board', boardId]);
  }

  removeBoard(boardId: number) {
    const url = `https://localhost:7126/bugtracker/board/${boardId}`;
    const confirmDelete = confirm('Are you sure you want to delete this board?');

    if (confirmDelete) {
      this.http.delete<BoardData>(url)
        .subscribe(() => {
          this.router.navigate(['/boards']);
          this.getData();
        });
    } else {
      this.router.navigate(['/boards']);
    }
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
}
