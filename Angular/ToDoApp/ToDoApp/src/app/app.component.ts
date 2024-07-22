import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { RegisterLoginComponent } from './register-login/register-login.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ActiveComponent } from './active/active.component';
import { HeaderComponent } from './header/header.component';
import { AddtaskComponent } from './addtask/addtask.component';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RegisterLoginComponent, SidebarComponent, DashboardComponent,
            ActiveComponent, HeaderComponent, AddtaskComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ToDoApp';
  isLoading : boolean=true;

}
