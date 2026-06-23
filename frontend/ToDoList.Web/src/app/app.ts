import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TodoListPage } from './features/todos/pages/todo-list-page/todo-list-page';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TodoListPage],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App {}
