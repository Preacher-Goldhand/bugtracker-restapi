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
import {Select2Group} from 'ng-select2-component';
import { UserDetails } from '../../../app/model/user-details';

@Component({
  selector: 'app-task-add',
  templateUrl: './task-add.component.html',
  styleUrls: ['./task-add.component.css']
})
export class TaskAddComponent implements OnInit {
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
          this.selectGroupEmployeesData = this.mapToSelectGroup(result) as Select2Group[];
      });

    this.task.boardId = this._boardId ?? 0;
    this.task.assignerId = this.accountService.getUserDetails()?.id ?? 0;

    this.http.get("https://localhost:7126/bugtracker/employee/availableHours/3")
    .subscribe((result) => {
        console.log("Result", result);
    });
  }

  addTask(): void {
    const userDetails: UserDetails | undefined = this.accountService.getUserDetails();

    if (userDetails && userDetails.availableHours !== undefined) {
      if (+this.task.storyPoints > +userDetails.availableHours) {
        alert("Story Points exceed available hours.");
      } else {
        this.http.post(`https://localhost:7126/bugtracker/board/${this._boardId}/task`, this.task)
          .subscribe({
            next: value => {
              this.router.navigate(['/board-details', this._boardId]);
            },
            error: err => { }
          });

        this.accountService.getUserDetails();
      }
    } else {
      alert("User's available hours not defined.");
    }
  }

  cancelTask(): void {
    this.router.navigate(['/board-details', this._boardId]);
  }

  private mapToSelectGroup(data: EmployeeShortData[]): any {
    return data.map(item => ({
      value: item.id,
      label: item.fullName
    }))
  }
}
