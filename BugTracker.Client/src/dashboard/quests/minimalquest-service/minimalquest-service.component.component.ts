import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BoardData } from '../../models/board.model';
import { DetailedBoardData } from '../../models/detailed-board.model';

@Component({
  selector: 'app-minimalquest-service.component',
  templateUrl: './minimalquest-service.component.component.html',
  styleUrls: ['./minimalquest-service.component.component.css']
})
export class MinimalQuestServiceComponent implements OnInit {
  boardDetails: DetailedBoardData = {
    name: '',
    dateOfCreation: new Date(),
    boardTasks: []
  };

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private datePipe: DatePipe
  ) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      const boardId = params['id'];
      this.getBoardDetails(boardId);
    });
  }

  getBoardDetails(boardId: number) {
    const url = `https://localhost:7126/bugtracker/board/${boardId}`;

    this.http.get<DetailedBoardData>(url)
      .subscribe((result: DetailedBoardData) => {
        this.boardDetails = result;
      });
  }
  addQuest() { }
  showDetails() { }
  editTask() { }
  removeTask() { }

  formatDateString(date: Date): string {
    const options: Intl.DateTimeFormatOptions = { year: 'numeric', month: '2-digit', day: '2-digit' };
    return this.datePipe.transform(date, 'shortDate')!;
  }
}
