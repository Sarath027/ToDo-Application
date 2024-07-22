import { Component, EventEmitter, Output } from '@angular/core';
import { RouterLinkActive } from '@angular/router';
import { RouterLink } from '@angular/router';
import { AddtaskButtonComponent } from '../addtask-button/addtask-button.component';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterLinkActive, RouterLink, AddtaskButtonComponent],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  @Output() clickedMessage = new EventEmitter<boolean>();
  isClicked : boolean = false;
  addTask(){
    this.isClicked = true;
    this.clickedMessage.emit(this.isClicked);
  }
}
