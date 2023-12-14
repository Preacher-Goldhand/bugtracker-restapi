import { Component, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { MyTask } from "../../models/my-task";
import { PagedResult } from "../../models/paged-result.model";
import { TaskCategoriesMap, TaskPrioritiesMap, TaskStatusesMap } from "../../models/consts";
import { DetailedBoardData } from "../../models/detailed-board.model";
import { Router } from "@angular/router";

@Component({
  selector: 'app-list-add',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {
  tasks: MyTask[] = [];

  taskStatusesMap = TaskStatusesMap;
  taskCategoriesMap = TaskCategoriesMap;
  taskPrioritiesMap = TaskPrioritiesMap;

  indexDescription: number | undefined;

  pageNumber: number = 1;
  pageSize: number = 5;
  tasksLoaded: boolean = false;
  private totalPages: number = 0;

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit(): void {
    this.getData();
  }

  getData(): void {
    const url = `https://localhost:7126/bugtracker/mytasks?pageSize=${this.pageSize}&pageNumber=${this.pageNumber}`;
    this.http.get<PagedResult<MyTask>>(url)
      .subscribe((result: PagedResult<MyTask>) => {
        this.tasks = result.items;
        this.tasksLoaded = true;
      });
  }

  showDescription(taskId: number): void {
    if (this.indexDescription) {
      this.indexDescription = undefined;
    } else {
      this.indexDescription = taskId;
    }
  }

  editQuest(boardId: number, taskId: any) {
    this.router.navigate(['/task-edit', boardId, taskId]);
  }
 
  goBoards(): void {
    this.router.navigate(['/boards']);
  }

  updatePage() {
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
    this.pageSize = + pageSize;
    this.pageNumber = 1;
    this.getData();
  }
}
