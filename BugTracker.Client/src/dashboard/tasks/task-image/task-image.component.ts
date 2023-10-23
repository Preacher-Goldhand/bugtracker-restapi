import {Component, Input, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {DomSanitizer, SafeResourceUrl} from "@angular/platform-browser";

@Component({
  selector: 'app-task-image',
  templateUrl: './task-image.component.html',
  styleUrls: ['./task-image.component.css']
})
export class TaskImageComponent implements OnInit {
  @Input() fileName: string | undefined;

  fileContent: SafeResourceUrl | undefined;

  constructor(private http: HttpClient, private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    console.log(this.fileName);
    this.getFile(this.fileName);
  }

  private getFile(name?: string): void {
    if (name) {
      this.http.get(`https://localhost:7126/bugtracker/file?fileName=${name}`,
        {responseType: "text"})
        .subscribe((baseImage: any) => {
          this.fileContent = this.sanitizer.bypassSecurityTrustResourceUrl(baseImage);
        });
    }
  }
}
