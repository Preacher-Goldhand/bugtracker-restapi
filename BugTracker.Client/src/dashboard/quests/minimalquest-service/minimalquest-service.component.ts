import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DetailedBoardData } from '../../models/detailed-board.model';
import { MinimalQuestData } from '../../models/minimal-quest.model';

@Component({
  selector: 'app-minimalquest-service.component',
  templateUrl: './minimalquest-service.component.html',
  styleUrls: ['./minimalquest-service.component.css']
})
export class MinimalQuestServiceComponent implements OnInit {
  boardDetails: DetailedBoardData = {
    name: '',
    dateOfCreation: new Date(),
    boardTasks: []
  };

  searchPhrase: string = '';
  filteredQuests: MinimalQuestData[] = [];
  noResultsMessage: string = '';
  pageSize: number = 5;
  pageNumber: number = 1;
  totalPages: number = 0;

  constructor(private route: ActivatedRoute, private http: HttpClient, private datePipe: DatePipe) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      const boardId = params['id'];
      this.getData(boardId);
    });
  }

  getData(boardId: number) {
    const url = `https://localhost:7126/bugtracker/board/${boardId}`;

    this.http.get<DetailedBoardData>(url)
      .subscribe((result: DetailedBoardData) => {
        this.boardDetails = result;
      });
  }

  searchQuest() {
    if (this.searchPhrase === '') {
      this.filteredQuests = [];
      this.noResultsMessage = '';
      return;
    }

    this.filteredQuests = this.boardDetails.boardTasks.filter(task =>
      task.name.toLowerCase().includes(this.searchPhrase.toLowerCase()) ||
      task.category.toLowerCase().includes(this.searchPhrase.toLowerCase()) ||
      task.priority.toString().includes(this.searchPhrase) ||
      task.taskStatus.toLowerCase().includes(this.searchPhrase.toLowerCase())
    );
 
    if (this.filteredQuests.length === 0) {
      this.noResultsMessage = 'No results found for the entered search phrase';
    } else {
      this.noResultsMessage = '';
    }
    this.updatePagedQuests();
  }

  addQuest() { }
  showQuestDetails() { }
  editQuest() { }
  removeQuest() { }

  updatePage() {
    this.pageNumber = 1;
    this.searchQuest();
  }

  previousPage() {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.searchQuest();
    }
  }

  nextPage() {
    if (this.pageNumber < this.totalPages) {
      this.pageNumber++;
      this.searchQuest();
    }
  }

  updatePagedQuests() {
    const startIndex = (this.pageNumber - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.filteredQuests = this.filteredQuests.slice(startIndex, endIndex);
  }

  formatDateString(date: Date): string {
    const options: Intl.DateTimeFormatOptions = { year: 'numeric', month: '2-digit', day: '2-digit' };
    return this.datePipe.transform(date, 'shortDate')!;
  }
}
