import { NgModule } from "@angular/core";
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AngularMaterialModule } from './../material.module';

import { SetupComponent } from './setup/setup.component';
import { PlanEntryComponent } from './plan-entry/plan-entry.component';
import { PlanEntryDeleteComponent } from './plan-entry-delete/plan-entry-delete.component';
import { PlanListComponent } from './plan-list/plan-list.component';
import { DialogBodyDirective } from './dialogs/dialog-body.directive';
import { BoundDialogComponent } from './dialogs/bound-dialog.component';
import { ValidationPanelComponent } from './validation-panel/validation-panel.component';

@NgModule({
  declarations: [
    SetupComponent,
    PlanEntryComponent,
    PlanEntryDeleteComponent,
    PlanListComponent,
    ValidationPanelComponent,
    BoundDialogComponent,
    DialogBodyDirective
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AngularMaterialModule,
    RouterModule.forChild([
      { path: '', component: SetupComponent }
    ])
  ],
  exports: [SetupComponent, RouterModule],
  providers: []
})
export class PlanningModule { }
