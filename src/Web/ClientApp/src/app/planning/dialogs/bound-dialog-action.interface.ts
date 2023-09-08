import { Subject } from 'rxjs';

export interface BoundDialogAction {
    name: string;
    color?: string;
    callback: () => Subject<boolean>;
    isDisabledCallback?: () => boolean;
}
