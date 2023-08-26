import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BoardData } from '../../models/board.model';
import { UpdateBoardData } from '../../models/update-board.model';

@Component({
  selector: 'app-update-board',
  templateUrl: './update-board.component.html',
  styleUrls: ['./update-board.component.css']
})
export class UpdateBoardComponent {

  updatedBoards: UpdateBoardData = { Name: '' };
  boards: BoardData[] = [];

  constructor(private route: ActivatedRoute, private http: HttpClient, private router: Router) { }

  updateBoard(board: BoardData) {
    const url = `https://localhost:7126/bugtracker/board/${board.id}`;


    const updateData: UpdateBoardData = {
      Name: this.updatedBoards.Name
    };

    this.http.put<UpdateBoardData>(url, updateData)
      .subscribe(() => {
        
      });
  }
}
