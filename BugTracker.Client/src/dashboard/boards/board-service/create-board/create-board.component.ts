import { AccountService } from './../../../../app/services/account.service';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { CreateBoardData } from '../../../models/create-board.model';


@Component({
  selector: 'app-create-board',
  templateUrl: './create-board.component.html',
  styleUrls: ['./create-board.component.css']
})
export class CreateBoardComponent {
  createBoardData: CreateBoardData = {
    Name: '',
    DateOfCreation: new Date
  };

  constructor(private http: HttpClient, private router: Router, private accountService: AccountService) { }

  createBoard() {
    this.http.post('https://localhost:7126/bugtracker/board', this.createBoardData)
      .subscribe(
        response => {
          console.log('Sucess:', response); 
          this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
            this.router.navigate(['/boards']);
          });
        },
        error => {
          console.log('Fail:', error);
        }
      );
  }
  cancelEditBoard(): void {
      this.router.navigate(['/boards']);
  }
}
