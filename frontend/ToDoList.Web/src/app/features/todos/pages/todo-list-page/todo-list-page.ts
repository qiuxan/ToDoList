import { Component, inject, signal } from '@angular/core';
import { TodoApi } from '../../../../core/api/todo-api';
import { Todo } from '../../../../core/models/todo';
import { AddTodoForm, AddTodoFormValue } from '../../components/add-todo-form/add-todo-form';

@Component({
  selector: 'app-todo-list-page',
  imports: [AddTodoForm],
  templateUrl: './todo-list-page.html',
  styleUrl: './todo-list-page.scss',
})
export class TodoListPage {
  private readonly todoApi = inject(TodoApi);

  readonly createdTodo = signal<Todo | null>(null);
  readonly errorMessage = signal<string | null>(null);
  readonly isCreating = signal(false);

  onCreateRequested(value: AddTodoFormValue): void {
    this.errorMessage.set(null);
    this.isCreating.set(true);

    this.todoApi.createTodo(value).subscribe({
      next: (todo) => {
        this.createdTodo.set(todo);
        this.isCreating.set(false);
      },
      error: () => {
        this.errorMessage.set('Could not create task. Please try again.');
        this.isCreating.set(false);
      },
    });
  }
}
