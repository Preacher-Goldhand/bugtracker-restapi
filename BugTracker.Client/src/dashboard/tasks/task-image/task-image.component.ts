import {Component, Input} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {saveAs} from "file-saver";
import {TaskComment} from "../../models/task-comment";

@Component({
  selector: 'app-task-image',
  templateUrl: './task-image.component.html',
  styleUrls: ['./task-image.component.css']
})
export class TaskImageComponent {
  @Input() boardId: number | undefined;
  @Input() comment: TaskComment | undefined;

  constructor(private http: HttpClient) { }

  downloadFile(): void {
    if (this.comment?.fileName) {
      this.http.get(`https://localhost:7126/bugtracker/file?fileName=${this.comment.fileName}`,
        { responseType: "blob" })
        .subscribe((response: any) => {
          saveAs(response, this.comment?.fileName);
        });
    }
  }

  removeFile(): void {
    if (this.comment?.fileName) {
      this.http.delete(`https://localhost:7126/bugtracker/file?fileName=${this.comment.fileName}`)
        .subscribe((response: any) => {
          this.removeFileFromComment();
          if (this.comment) {
            this.comment.fileName = '';
          }
        });
    }
  }

  private removeFileFromComment(): void {
    const url = `https://localhost:7126/bugtracker/board/${this.boardId}/task/${this.comment?.taskId}/comment`;
    if (this.comment) {
      this.comment.fileName = '';
    }
    this.http.put(url, this.comment).subscribe({
      next: value => {
      },
      error: err => {
      }
    });
  }
}
