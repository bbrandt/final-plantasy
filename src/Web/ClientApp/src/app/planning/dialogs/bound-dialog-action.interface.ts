import { Subject } from 'rxjs';

export interface BoundDialogAction {
    name: string;
    callback: () => Subject<boolean>;
    isDisabledCallback?: () => boolean;
}
