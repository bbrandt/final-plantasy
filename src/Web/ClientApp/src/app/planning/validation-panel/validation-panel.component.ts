import { Component, Input } from '@angular/core';
import { ValidationMessage } from './../model/validation-message';
import { ValidationType } from './../model/validation-type';

@Component({
  selector: 'validation-panel',
  styleUrls: ['./validation-panel.component.css'],
  templateUrl: './validation-panel.component.html'
})
export class ValidationPanelComponent {
  @Input()
  public messages: ValidationMessage[] = [];

  constructor() {
  }

  public getErrorMessages(): ValidationMessage[] {
    return this.messages.filter(x => x.validationType === ValidationType.Error);
  }
}
