import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Subject, take } from 'rxjs';
import { PlanEntryService } from './../services/plan-entry.service';
import { PlanListDataSource } from './plan-list.datasource';
import { PlanEntryModel } from './../model/plan-entry.model';

@Component({
  selector: 'plan-list',
  templateUrl: './plan-list.component.html',
  styleUrls: ['./plan-list.component.css']
})
export class PlanListComponent implements OnInit, OnChanges {
  readonly #planEntryService: PlanEntryService;
  public dataSource!: PlanListDataSource;

  @Input()
  public refreshSubject!: Subject<boolean>;

  @Input()
  public communicator!: PlanListComponentCommunicator;

  public displayedColumns = ["actions", "planType", "eventDate", "endDate", "amount", "repeatOn", "description"];

  constructor(
    planEntryService: PlanEntryService)
  {
    this.#planEntryService = planEntryService;
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.checkRefreshSubject();
  }

  public ngOnInit(): void {
    this.dataSource = new PlanListDataSource(this.#planEntryService);
    this.dataSource.loadPlanEntries();
  }

  public editPlan(row: PlanEntryModel) {
    this.communicator.editClick(row);
  }

  public deletePlan(row: PlanEntryModel) {
    this.communicator.deleteClick(row);
  }

  private checkRefreshSubject(): void {
    const subscription$ = this.refreshSubject
      .pipe(take(1))
      .subscribe((shouldRefresh) => {
        if (shouldRefresh) {
          this.dataSource.loadPlanEntries();

          subscription$.unsubscribe();
        }
    });
  }
}

export interface PlanListComponentCommunicator {
  editClick: (model: PlanEntryModel) => void;
  deleteClick: (model: PlanEntryModel) => void;
}
