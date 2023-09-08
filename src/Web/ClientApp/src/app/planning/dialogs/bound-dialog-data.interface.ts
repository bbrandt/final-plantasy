import { Type } from '@angular/core';
import { BoundDialogAction } from './bound-dialog-action.interface';

export interface BoundDialogData {
    boundComponent: Type<any>;
    title?: string;
    actions: BoundDialogAction[];
}
