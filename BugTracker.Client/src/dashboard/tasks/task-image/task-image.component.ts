import {Component, Input} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {saveAs} from "file-saver";

@Component({
  selector: 'app-task-image',
  templateUrl: './task-image.component.html',
  styleUrls: ['./task-image.component.css']
})
export class TaskImageComponent {
  @Input() fileName: string | undefined;

  constructor(private http: HttpClient) { }

  downloadFile(): void {
    if (this.fileName) {
      this.http.get(`https://localhost:7126/bugtracker/file?fileName=${this.fileName}`,
        { responseType: "blob" })
        .subscribe((response: any) => {
          saveAs(response, this.fileName);
        });
    }
  }
}
