import { Routes } from '@angular/router';
import { RegisterLoginComponent } from './register-login/register-login.component';
import { ActiveComponent } from './active/active.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AddtaskComponent } from './addtask/addtask.component';
import { HomeComponent } from './home/home.component';
import { authGuard } from './Guards/auth.guard';

export const routes: Routes = [
    {
        path: '', pathMatch : 'full', redirectTo: 'login',
    },
    {
        path : 'login', component: RegisterLoginComponent
    },
    {
        path : 'signup', component: RegisterLoginComponent
    },
    {
        path : 'home', component : HomeComponent, canActivate: [authGuard],  children:[
            {
                path : 'addtask', component : AddtaskComponent, 
            },
            {
                path : 'dashboard', component: DashboardComponent,
            },
            {
                path : 'active', component: ActiveComponent
            },
            {
                path : 'completed', component: ActiveComponent,
            }
        ]
    },
    
];
