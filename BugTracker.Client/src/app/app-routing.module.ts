import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from '../account/home/home.component';
import { LoginComponent } from '../account/login/login.component';
import { RegisterComponent } from '../account/register/register.component';
import { ChangePasswordComponent } from '../account/change-password/change-password.component';
import { BoardServiceComponent } from '../dashboard/boards/boardservice/boardservice.component';
import { HomeDashboardComponent } from '../dashboard/home-dashboard/home-dashboard.component';
import { CreateBoardComponent } from '../dashboard/boards/boardservice/create-board/create-board.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'change-password', component: ChangePasswordComponent },
  { path: 'dashboard', component: HomeDashboardComponent },
  { path: 'boards', component: BoardServiceComponent },
  { path: 'add-board', component: CreateBoardComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
