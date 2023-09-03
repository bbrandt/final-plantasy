import { NgModule } from "@angular/core";
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AngularMaterialModule } from './../material.module';

import { SetupComponent } from './setup/setup.component';
import { PlanEntryComponent } from './plan-entry/plan-entry.component';
import { PlanListComponent } from './plan-list/plan-list.component';
import { PlanEntryService } from './services/plan-entry.service';
import { DialogBodyDirective } from './dialogs/dialog-body.directive';
import { BoundDialogComponent } from './dialogs/bound-dialog.component';

@NgModule({
  declarations: [
    SetupComponent,
    PlanEntryComponent,
    PlanListComponent,
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
  providers: [PlanEntryService]
})
export class PlanningModule { }
