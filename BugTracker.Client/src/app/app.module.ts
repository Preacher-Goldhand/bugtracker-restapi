import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { AppComponent } from './app.component';
import { LoginComponent } from '../account/login/login.component'
import { HomeComponent } from '../account/home/home.component'
import { RegisterComponent } from '../account/register/register.component';
import { ChangePasswordComponent } from '../account/change-password/change-password.component';
import { DatePipe } from '@angular/common';
import { HomeDashboardComponent } from '../dashboard/home-dashboard/home-dashboard.component';
import { BoardServiceComponent } from '../dashboard/boards/board-service/board-service.component';
import { CreateBoardComponent } from '../dashboard/boards/board-service/create-board/create-board.component';
import { MinimalQuestServiceComponent } from '../dashboard/quests/minimalquest-service/minimalquest-service.component';
import { UpdateBoardComponent } from '../dashboard/boards/update-board/update-board.component';
import { LogoutComponent } from '../account/logout/logout.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {TaskAddComponent} from "../dashboard/tasks/task-add/task-add.component";
import {TaskCommentComponent} from "../dashboard/tasks/task-comment/task-comment.component";
import {TaskImageComponent} from "../dashboard/tasks/task-image/task-image.component";
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { Select2Module } from 'ng-select2-component';
import {TaskListComponent} from "../dashboard/tasks/task-list/task-list.component";
import { TaskEditComponent } from '../dashboard/tasks/task-edit/task-edit.component';
import { EmployeesComponent } from '../dashboard/employees/employees.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    ChangePasswordComponent,
    LogoutComponent,
    HomeDashboardComponent,
    BoardServiceComponent,
    CreateBoardComponent,
    MinimalQuestServiceComponent,
    UpdateBoardComponent,
    TaskAddComponent,
    TaskCommentComponent,
    TaskImageComponent,
    TaskListComponent,
    TaskEditComponent,
    EmployeesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    NgbModule,
    BrowserAnimationsModule,
    Select2Module
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
