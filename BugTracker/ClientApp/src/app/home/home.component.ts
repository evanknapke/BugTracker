import { Component } from '@angular/core';
import {CdkDragDrop, moveItemInArray, transferArrayItem} from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  
  issue: String;
  todo = [];
  inProgress = [];
  done = [];
  
  addToTodo() {
    this.todo.push( this.issue );
    this.issue = ''; // clear input field
  }

  deleteIssue(issue, list) {
    const index = list.indexOf(issue);
    if (index > -1) {
      list.splice(index, 1);
    }
  }

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(event.previousContainer.data,
                        event.container.data,
                        event.previousIndex,
                        event.currentIndex);
    }
  }
}