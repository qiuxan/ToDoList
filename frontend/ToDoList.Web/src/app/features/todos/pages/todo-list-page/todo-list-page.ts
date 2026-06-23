import { Component } from '@angular/core';
import { AddTodoForm, AddTodoFormValue } from '../../components/add-todo-form/add-todo-form';

@Component({
  selector: 'app-todo-list-page',
  imports: [AddTodoForm],
  templateUrl: './todo-list-page.html',
  styleUrl: './todo-list-page.scss',
})
export class TodoListPage {
  onCreateRequested(value: AddTodoFormValue): void {
    console.log('create requested from page', value);
  }
}
