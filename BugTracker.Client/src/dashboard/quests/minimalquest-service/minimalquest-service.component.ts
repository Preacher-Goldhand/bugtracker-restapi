import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DetailedBoardData } from '../../models/detailed-board.model';
import { TaskCategoriesMap, TaskPrioritiesMap, TaskStatusesMap } from "../../models/consts";
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

  taskDetails: MinimalQuestData | undefined;
  indexDescription: number | undefined;

  searchPhrase: string = '';
  filteredQuests: MinimalQuestData[] = [];
  noResultsMessage: string = '';
  pageSize: number = 5;
  pageNumber: number = 1;
  totalPages: number = 0;

  taskStatusesMap = TaskStatusesMap;
  taskCategoriesMap = TaskCategoriesMap;
  taskPrioritiesMap = TaskPrioritiesMap;

  private _boardId!: number;

  constructor(private route: ActivatedRoute,
    private http: HttpClient,
    private datePipe: DatePipe,
    private router: Router) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this._boardId = params['id'];;
      this.getData(this._boardId);
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

  addQuest(): void {
    this.router.navigate(['/task-add', this._boardId]);
  }
  editQuest(taskId: any) {
    this.router.navigate(['/task-edit', this._boardId, taskId]);
  }
  removeQuest(taskId: any) {
    const url = `https://localhost:7126/bugtracker/board/${this._boardId}/task/${taskId}`;
    const confirmDelete = confirm('Are you sure you want to delete this task');

    if (confirmDelete) {
      this.http.delete<DetailedBoardData>(url)
        .subscribe(() => {
          this.router.navigate(['/boards']);
          this.getData(this._boardId);
        });
    } else {
      this.router.navigate(['/boards']);
    }
  }

  showComments(taskId: any): void {
    this.router.navigate(['/task-comment', this._boardId, taskId]);
  }

  showDescription(taskId: number): void {
    if (this.indexDescription) {
      this.indexDescription = undefined;
    } else {
      this.indexDescription = taskId;
    }
  }

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
