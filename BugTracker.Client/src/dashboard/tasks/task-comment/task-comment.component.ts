import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {TaskComment} from "../../models/task-comment";
import {AccountService} from "../../../app/services/account.service";
import { Task } from 'src/dashboard/models/task.model';

@Component({
  selector: 'app-task-comment',
  templateUrl: './task-comment.component.html',
  styleUrls: ['./task-comment.component.css']
})
export class TaskCommentComponent implements OnInit {

  task: Task = {} as Task;
  taskComments: TaskComment[] = [];

  taskComment: TaskComment = {
    dateOfCreation: new Date(),
    description: '',
    userCreatedId: 0
  };

  private _boardId!: number;
  private _taskId!: number;

  constructor(private route: ActivatedRoute,
              private http: HttpClient,
              private router: Router,
              private accountService: AccountService) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this._boardId = params['boardId'];
      this._taskId = params['taskId'];
      this.getTaskDetails(this._boardId, this._taskId);
      this.getComments(this._boardId, this._taskId);
    });
  }

  addComment(): void {
    this.taskComment.userCreatedId = this.accountService.getUserDetails()?.id ?? 0;
    const url = `https://localhost:7126/bugtracker/board/${this._boardId}/task/${this._taskId}/comment`;
    this.http.post(url, this.taskComment)
      .subscribe({
        next: value => {
          this.getComments(this._boardId, this._taskId);
        },
        error: err => { }
      })
  }

  cancelComments(): void {
    this.router.navigate(['/board-details', this._boardId]);
  }

  private getTaskDetails(boardId: number, taskId: number): void {
    const url = `https://localhost:7126/bugtracker/board/${boardId}/task/${taskId}`;
    this.http.get<Task>(url)
      .subscribe({
        next: value => {
          this.task = value;
        },
        error: err => { }
      });
  }

  private getComments(boardId: number, taskId: number): void {
    const url = `https://localhost:7126/bugtracker/board/${boardId}/task/${taskId}/comments`;
    this.http.get<TaskComment[]>(url)
      .subscribe({
        next: value => {
          this.taskComments = value;
        },
        error: err => { }
      });
  }
}
