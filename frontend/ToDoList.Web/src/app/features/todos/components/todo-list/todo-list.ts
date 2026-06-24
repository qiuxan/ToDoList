import { Component, input, output, computed } from '@angular/core';
import { Todo } from '../../../../core/models/todo';

export interface TodoCompletionChange {
  id: string;
  isCompleted: boolean;
}
@Component({
  selector: 'app-todo-list',
  imports: [],
  templateUrl: './todo-list.html',
  styleUrl: './todo-list.scss',
})
export class TodoList {
  readonly todos = input.required<Todo[]>();

  readonly activeTodos = computed(() => this.todos().filter((todo) => !todo.isCompleted));

  readonly completedTodos = computed(() => this.todos().filter((todo) => todo.isCompleted));

  readonly completionChangeRequested = output<TodoCompletionChange>();

  readonly deleteRequested = output<string>();
}
