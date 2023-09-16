import { NgModule } from "@angular/core";
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AngularMaterialModule } from './../material.module';

import { DialogBodyDirective } from './dialogs/dialog-body.directive';
import { BoundDialogComponent } from './dialogs/bound-dialog.component';
import { ValidationPanelComponent } from './validation-panel/validation-panel.component';

@NgModule({
  declarations: [
    BoundDialogComponent,
    ValidationPanelComponent,
    DialogBodyDirective
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AngularMaterialModule
  ],
  exports: [ValidationPanelComponent, BoundDialogComponent, DialogBodyDirective ],
  providers: []
})
export class SharedModule { }
