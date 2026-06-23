import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { CreateTodoRequest, Todo } from '../models/todo';

@Service()
export class TodoApi {
  private readonly http = inject(HttpClient);

  createTodo(request: CreateTodoRequest) {
    return this.http.post<Todo>('/api/todos', request);
  }
}