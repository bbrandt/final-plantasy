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
import { PlanTimelineComponent } from './plan-timeline/plan-timeline.component';

import * as PlotlyJS from 'plotly.js-dist-min';
import { PlotlyModule } from 'angular-plotly.js';

PlotlyModule.plotlyjs = PlotlyJS;

@NgModule({
  declarations: [
    SetupComponent,
    PlanEntryComponent,
    PlanEntryDeleteComponent,
    PlanListComponent,
    ValidationPanelComponent,
    PlanTimelineComponent,
    BoundDialogComponent,
    DialogBodyDirective
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AngularMaterialModule,
    PlotlyModule,
    RouterModule.forChild([
      { path: 'setup', component: SetupComponent },
      { path: 'timeline', component: PlanTimelineComponent }
    ])
  ],
  exports: [SetupComponent, RouterModule],
  providers: []
})
export class PlanningModule { }
