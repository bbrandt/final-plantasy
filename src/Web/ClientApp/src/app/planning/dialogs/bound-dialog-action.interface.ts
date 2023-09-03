export interface BoundDialogAction {
    name: string;
    callback: () => boolean;
    isDisabledCallback?: () => boolean;
}
