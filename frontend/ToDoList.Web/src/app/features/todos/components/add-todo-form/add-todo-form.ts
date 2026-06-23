import { Component, output, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';

export interface AddTodoFormValue {
  title: string;
  description: string | null;
}

@Component({
  selector: 'app-add-todo-form',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './add-todo-form.html',
  styleUrls: ['./add-todo-form.scss'],
})
export class AddTodoForm {
  readonly createRequested = output<AddTodoFormValue>();
  readonly title = signal('');
  readonly description = signal('');

  submit(): void {
    const title = this.title().trim();
    const description = this.description().trim();

    if (!title) {
      return;
    }

    this.createRequested.emit({
      title,
      description: description || null,
    });

    this.title.set('');
    this.description.set('');
  }
}
