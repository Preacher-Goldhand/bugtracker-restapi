import { Component, OnInit } from '@angular/core';
import { UserDetails } from './model/user-details';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  userLogin: boolean = false;
  userName: string = '';

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.accountService.getUserLogin().subscribe((value) => {
      this.userLogin = value;

      if (this.userLogin) {
        const userDetails: UserDetails = this.accountService.getUserDetails();
        this.userName = userDetails.name || '';
      }
    });
  }

  title = 'BugTracker.Client v 1.0';
}
