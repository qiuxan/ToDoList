import { Component, inject, signal, OnInit } from '@angular/core';
import { TodoApi } from '../../../../core/api/todo-api';
import { Todo } from '../../../../core/models/todo';
import { AddTodoForm, AddTodoFormValue } from '../../components/add-todo-form/add-todo-form';
import { TodoList } from '../../components/todo-list/todo-list';

@Component({
  selector: 'app-todo-list-page',
  imports: [AddTodoForm, TodoList],
  templateUrl: './todo-list-page.html',
  styleUrl: './todo-list-page.scss',
})
export class TodoListPage implements OnInit {
  private readonly todoApi = inject(TodoApi);

  readonly todos = signal<Todo[]>([]);

  readonly errorMessage = signal<string | null>(null);
  readonly isCreating = signal(false);
  readonly isLoading = signal(false);

  ngOnInit(): void {
    this.loadTodos();
  }

  onCreateRequested(value: AddTodoFormValue): void {
    this.errorMessage.set(null);
    this.isCreating.set(true);

    this.todoApi.createTodo(value).subscribe({
      next: (todo) => {
        this.todos.update((todos) => [todo, ...todos]);
        this.isCreating.set(false);
      },
      error: () => {
        this.errorMessage.set('Could not create task. Please try again.');
        this.isCreating.set(false);
      },
    });
  }

  private loadTodos(): void {
    this.errorMessage.set(null);
    this.isLoading.set(true);

    this.todoApi.getTodos().subscribe({
      next: (todos) => {
        this.todos.set(todos);
        this.isLoading.set(false);
      },
      error: () => {
        this.errorMessage.set('Could not load tasks. Please refresh the page.');
        this.isLoading.set(false);
      },
    });
  }
}
