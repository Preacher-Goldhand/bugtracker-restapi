import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Select2Group } from 'ng-select2-component';
import { UserDetails } from '../../../app/model/user-details';
import { AccountService } from '../../../app/services/account.service';
import { TaskCategoriesMap, TaskPrioritiesMap, TaskStatusesMap } from '../../models/consts';
import { EmployeeData } from '../../models/employee-model';
import { EmployeeShortData } from '../../models/employee-short-data';
import { TaskCategory } from '../../models/task-category';
import { TaskPriority } from '../../models/task-priority';
import { TaskStatus } from '../../models/task-status';
import { Task } from '../../models/task.model';

@Component({
  selector: 'app-task-edit',
  templateUrl: './task-edit.component.html',
  styleUrls: ['./task-edit.component.css']
})
export class TaskEditComponent implements OnInit {
  task: Task = {
    boardId: 0,
    assigneeId: 0,
    assignerId: 0,
    category: TaskCategory.ADMINISTRATIVE_TASK,
    dateOfCreation: new Date(),
    description: "",
    loggedTime: 0,
    storyPoints: 4,
    name: "",
    priority: TaskPriority.LOW,
    proposalDate: new Date(),
    taskStatus: TaskStatus.TO_DO
  };

  employee: EmployeeData = {
    id: 0,
    firstName: '',
    lastName: '',
    department: '',
    employeeEmail: '',
    employeePhoneNumber: '',
    availableHours: 0,
    employeeStatus: '',
    roleId: 0,
  };

  taskStatuses: TaskStatus[] = [TaskStatus.TO_DO, TaskStatus.IN_PROGRESS, TaskStatus.IN_TESTING, TaskStatus.DONE, TaskStatus.CLOSED];
  taskCategories: TaskCategory[] = [TaskCategory.ADMINISTRATIVE_TASK, TaskCategory.ANALYTIC_TASK, TaskCategory.BUGFIX_TASK,
  TaskCategory.DEVELOPMENT_TASK, TaskCategory.DEVOPS_TASK, TaskCategory.TESTING_TASK];
  taskPriorities: TaskPriority[] = [TaskPriority.LOW, TaskPriority.HIGH, TaskPriority.CRITICAL];
  taskStoryPoints: number[] = [4, 8, 10, 14, 20, 30, 40, 100];

  selectGroupEmployeesData: Select2Group[] = [];

  taskStatusesMap = TaskStatusesMap;
  taskCategoriesMap = TaskCategoriesMap;
  taskPrioritiesMap = TaskPrioritiesMap;

  private _boardId: number | undefined;
  private _taskId: number | undefined;

  constructor(private route: ActivatedRoute,
    private http: HttpClient,
    private router: Router,
    private accountService: AccountService) { }
  

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this._boardId = params['boardId'];
      this._taskId = params['taskId'];

      const url = `https://localhost:7126/bugtracker/employee/short`;
      this.http.get<EmployeeShortData[]>(url)
        .subscribe((result: EmployeeShortData[]) => {
          this.selectGroupEmployeesData = this.mapToSelectGroup(result) as Select2Group[];
        });

      if (this._boardId !== undefined && this._taskId !== undefined) {
        this.http.get<Task>(`https://localhost:7126/bugtracker/board/${this._boardId}/task/${this._taskId}`)
          .subscribe((data) => {
            this.task = data;
          });
      }

      this.task.boardId = this._boardId ?? 0;
      this.task.assignerId = this.accountService.getUserDetails()?.id ?? 0;
    });
  }

  editTask(): void {
    const userDetails: UserDetails | undefined = this.accountService.getUserDetails();

    if (userDetails) {
      this.http.get(`https://localhost:7126/bugtracker/employee/availableHours/${this.task.assigneeId}`)
        .subscribe((availableHours) => {

          if (+this.task.storyPoints > +availableHours) {
            alert("Story Points exceed available hours.");
          }
          else {
            this.http.put(`https://localhost:7126/bugtracker/board/${this._boardId}/task/${this._taskId}`, this.task)
              .subscribe({
                next: value => {
                  this.router.navigate(['/board-details', this._boardId]);
                },
                error: err => { }
          });

          this.accountService.getUserDetails();
          }
        });
    }
    else {
      alert("User's available hours not defined.");
    }
  }

  cancelEditTask(): void {
    if (this._boardId !== undefined) {
      this.router.navigate(['/board-details', this._boardId]);
    } else {
      console.error('_boardId is undefined. Cannot navigate.');
    }
  }

  private mapToSelectGroup(data: EmployeeShortData[]): any {
    return data.map(item => ({
      value: item.id,
      label: item.fullName
    }));
  }
}
