import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[dialogBody]'
})
export class DialogBodyDirective {
  public viewContainerRef: ViewContainerRef;

  constructor(viewContainerRef: ViewContainerRef) {
    this.viewContainerRef = viewContainerRef;
  }
}
