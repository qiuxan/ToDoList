import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { CreateTodoRequest, Todo } from '../models/todo';

@Service()
export class TodoApi {
  private readonly http = inject(HttpClient);

  getTodos() {
    return this.http.get<Todo[]>('/api/todos');
  }

  createTodo(request: CreateTodoRequest) {
    return this.http.post<Todo>('/api/todos', request);
  }

  deleteTodo(id: string) {
    return this.http.delete<void>(`/api/todos/${id}`);
  }

  completeTodo(id: string) {
    return this.http.patch<void>(`/api/todos/${id}/complete`, null);
  }

  incompleteTodo(id: string) {
    return this.http.patch<void>(`/api/todos/${id}/incomplete`, null);
  }
}
