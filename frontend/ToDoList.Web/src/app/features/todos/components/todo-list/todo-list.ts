import { Component, input, output } from '@angular/core';
import { Todo } from '../../../../core/models/todo';

@Component({
  selector: 'app-todo-list',
  imports: [],
  templateUrl: './todo-list.html',
  styleUrl: './todo-list.scss',
})
export class TodoList {
  readonly todos = input.required<Todo[]>();

  readonly deleteRequested = output<string>();
}
