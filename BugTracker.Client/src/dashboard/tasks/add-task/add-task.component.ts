import {Component, OnInit} from '@angular/core';
import {Task} from 'src/dashboard/models/task.model';
import {TaskPriority} from "../../models/task-priority";
import {TaskStatus} from "../../models/task-status";
import {TaskCategory} from "../../models/task-category";
import {ActivatedRoute, Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {EmployeeShortData} from "../../models/employee-short-data";
import {AccountService} from "../../../app/services/account.service";
import {TaskCategoriesMap, TaskPrioritiesMap, TaskStatusesMap} from "../../models/consts";

@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.css']
})
export class AddTaskComponent implements OnInit {
  task: Task = {
    boardId: 0,
    assigneeId: 0,
    assignerId: 0,
    category: TaskCategory.DEVELOPMENT_TASK,
    dateOfCreation: new Date(),
    description: "",
    loggedTime: 0,
    name: "",
    priority: TaskPriority.LOW,
    proposalDate: new Date(),
    taskStatus: TaskStatus.TO_DO
  };

  taskStatuses: TaskStatus[] = [TaskStatus.TO_DO];
  taskCategories: TaskCategory[] = [TaskCategory.DEVELOPMENT_TASK];
  taskPriorities: TaskPriority[] = [TaskPriority.LOW, TaskPriority.HIGH, TaskPriority.CRITICAL];

  employeesData: EmployeeShortData[] = [];

  taskStatusesMap = TaskStatusesMap;
  taskCategoriesMap = TaskCategoriesMap;
  taskPrioritiesMap = TaskPrioritiesMap;

  private _boardId: number | undefined;

  constructor(private route: ActivatedRoute,
              private http: HttpClient,
              private router: Router,
              private accountService: AccountService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this._boardId = params['id'];
    });

    const url = `https://localhost:7126/bugtracker/employee/short`;
    this.http.get<EmployeeShortData[]>(url)
      .subscribe((result: EmployeeShortData[]) => {
          this.employeesData = result;
      });

    this.task.boardId = this._boardId ?? 0;
    this.task.assignerId = this.accountService.getUserDetails()?.id ?? 0;
  }

  addTask(): void {
    this.http.post(`https://localhost:7126/bugtracker/board/${this._boardId}/task`, this.task)
      .subscribe({
        next: value => {
          this.router.navigate(['/board-details', this._boardId]);
        },
        error: err => {

        }
      })
  }

  cancelTask(): void {
    this.router.navigate(['/board-details', this._boardId]);
  }
}
