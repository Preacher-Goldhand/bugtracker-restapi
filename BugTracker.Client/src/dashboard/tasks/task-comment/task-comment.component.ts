import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
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
  @ViewChild('fileUpload') fileUpload!: ElementRef;

  task: Task = {} as Task;
  taskComments: TaskComment[] = [];

  taskComment: TaskComment = {
    dateOfCreation: new Date(),
    description: '',
    userCreatedId: 0
  };

  btnFileDelete: boolean = false;

  private _file: File | undefined;
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
    this.taskComment.fileName = this._file?.name;
    const url = `https://localhost:7126/bugtracker/board/${this._boardId}/task/${this._taskId}/comment`;
    this.http.post(url, this.taskComment)
      .subscribe({
        next: value => {
          this.uploadFile();
          this.getComments(this._boardId, this._taskId);
          this.reset();
        },
        error: err => { }
      })
  }

  cancelComments(): void {
    this.router.navigate(['/tasks']);
  }

  onFileSelected($event: any): void {
    this._file = $event.target.files[0];
  }

  uploadFile(): void {
    if (this._file) {
      const formData = new FormData();
      formData.append("file", this._file);

      this.http.post("https://localhost:7126/bugtracker/file", formData)
        .subscribe({
          next: value => {
          },
          error: err => {
            console.error(err);
          }
        });
    }
  }

  getFile(fileName?: string): void {
    if (fileName) {
      this.http.get(`https://localhost:7126/bugtracker/file?fileName=${fileName}`,
        { responseType: "text" })
        .subscribe((baseImage: any) => {
          return baseImage;
        });
    }
  }

  private reset(): void {
    this._file = undefined;
    this.taskComment.description = '';
    this.fileUpload.nativeElement.value = '';
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
