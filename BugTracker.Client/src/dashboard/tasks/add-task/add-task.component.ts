import {Component, OnInit} from '@angular/core';
import {Task} from 'src/dashboard/models/task.model';
import {TaskPriority} from "../../models/task-priority";
import {TaskStatus} from "../../models/task-status";
import {TaskCategory} from "../../models/task-category";
import {ActivatedRoute} from "@angular/router";

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

  private _boardId: number | undefined;

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this._boardId = params['id'];
    });
  }

  addTask(): void {

  }

  cancelTask(): void {

  }
}
