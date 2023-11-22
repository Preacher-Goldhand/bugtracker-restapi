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
import { TaskAddComponent } from "../dashboard/tasks/task-add/task-add.component";
import { TaskCommentComponent } from "../dashboard/tasks/task-comment/task-comment.component";
import { TaskListComponent } from "../dashboard/tasks/task-list/task-list.component";
import { TaskEditComponent } from '../dashboard/tasks/task-edit/task-edit.component';
import { EmployeesComponent } from '../dashboard/employees/employees.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'logout', component: LogoutComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'change-password', component: ChangePasswordComponent },
  { path: 'dashboard', component: HomeDashboardComponent },
  { path: 'boards', component: BoardServiceComponent },
  { path: 'add-board', component: CreateBoardComponent },
  { path: 'board-details/:id', component: MinimalQuestServiceComponent },
  { path: 'edit-board/:id', component: UpdateBoardComponent },
  { path: 'tasks', component: TaskListComponent },
  { path: 'task-add/:id', component: TaskAddComponent },
  { path: 'task-edit/:boardId/:taskId', component: TaskEditComponent },
  { path: 'task-comment/:boardId/:taskId', component: TaskCommentComponent },
  { path: 'employees', component: EmployeesComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
