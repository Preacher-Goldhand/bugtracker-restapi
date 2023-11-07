import {Component, OnInit} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {MyTask} from "../../models/my-task";
import {PagedResult} from "../../models/paged-result.model";
import {TaskCategoriesMap, TaskPrioritiesMap, TaskStatusesMap} from "../../models/consts";

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
  private totalPages: number = 0;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getData();
  }

  getData(): void {
    const url = `https://localhost:7126/bugtracker/mytasks?pageSize=${this.pageSize}&pageNumber=${this.pageNumber}`;
    this.http.get<PagedResult<MyTask>>(url)
      .subscribe((result: PagedResult<MyTask>) => {
        this.tasks = result.items;
      });
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
