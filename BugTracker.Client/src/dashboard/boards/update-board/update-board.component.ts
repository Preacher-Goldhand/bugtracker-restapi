import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BoardData } from '../../models/board.model';
import { UpdateBoardData } from '../../models/update-board.model';

@Component({
  selector: 'app-update-board',
  templateUrl: './update-board.component.html',
  styleUrls: ['./update-board.component.css']
})
export class UpdateBoardComponent implements OnInit {
  updatedBoard: UpdateBoardData = { Name: '' };
  board!: BoardData;

  constructor(private route: ActivatedRoute, private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      const boardId = params['id'];
      console.log('boardId:', boardId);

      const url = `https://localhost:7126/bugtracker/board/${boardId}`;
      this.http.get<BoardData>(url).subscribe((data) => {
        this.board = data;
        console.log('this.board:', this.board);
      });
    });
  }

  updateBoard() {
    const url = `https://localhost:7126/bugtracker/board/${this.board.id}`;

    const updateData: UpdateBoardData = {
      Name: this.updatedBoard.Name
    };

    this.http.put<UpdateBoardData>(url, updateData).subscribe(
      () => {
        console.log("Suckes")
      },
      (error) => {
        console.error('Błąd żądania PUT:', error);
      }
    );
  }
}
