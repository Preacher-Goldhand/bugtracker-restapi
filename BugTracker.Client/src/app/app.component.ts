import { Component, OnInit } from '@angular/core';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  userLogin: boolean | undefined;

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {
    this.accountService.getUserLogin().subscribe((value) => {
      this.userLogin = value;
    });
  }

  title = 'BugTracker.Client v 1.0';
}
