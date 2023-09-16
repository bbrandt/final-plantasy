import { ValidationType } from './validation-type';

export interface ValidationMessage {
  validationType: ValidationType;
  message: string;
}
