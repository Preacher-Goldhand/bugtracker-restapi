import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from '../account/home/home.component';
import { LoginComponent } from '../account/login/login.component';
import { RegisterComponent } from '../account/register/register.component';
import { ChangePasswordComponent } from '../account/change-password/change-password.component';
import { HomeDashboardComponent } from '../dashboard/home-dashboard/home-dashboard.component';
import { BoardServiceComponent } from '../dashboard/boards/board-service/board-service.component';
import { CreateBoardComponent } from '../dashboard/boards/board-service/create-board/create-board.component';
import { MinimalQuestServiceComponent } from '../dashboard/quests/minimalquest-service/minimalquest-service.component';
import { UpdateBoardComponent } from '../dashboard/boards/update-board/update-board.component';
import { LogoutComponent } from '../account/logout/logout.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'change-password', component: ChangePasswordComponent },
  { path: 'dashboard', component: HomeDashboardComponent },
  { path: 'boards', component: BoardServiceComponent },
  { path: 'add-board', component: CreateBoardComponent },
  { path: 'board-details/:id', component: MinimalQuestServiceComponent },
  { path: 'edit-board/:id', component: UpdateBoardComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
