import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {
  AddTodoForm,
  AddTodoFormValue,
} from './features/todos/components/add-todo-form/add-todo-form';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, AddTodoForm],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App {
  protected readonly title = signal('ToDoList.Web');
  onCreateRequested(value: AddTodoFormValue): void {
    console.log('create requested', value);
  }
}
