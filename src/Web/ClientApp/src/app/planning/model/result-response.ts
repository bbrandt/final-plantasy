import { ValidationMessage } from './validation-message';

export interface ResultResponse<T> {
  value?: T;
  messages: ValidationMessage[];
}
