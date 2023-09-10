import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Subject } from 'rxjs';
import { PlanEntryService } from './../services/plan-entry.service';
import { PlanListDataSource } from './plan-list.datasource';

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

  public displayedColumns = ["id", "planType", "eventDate", "amount", "repeatOn", "description"];

  constructor(planEntryService: PlanEntryService) {
    this.#planEntryService = planEntryService;
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.checkRefreshSubject();
  }

  public ngOnInit(): void {
    this.dataSource = new PlanListDataSource(this.#planEntryService);
    this.dataSource.loadPlanEntries();
  }

  public onRowClicked(row: any) {
    console.log(row);
  }

  private checkRefreshSubject(): void {
    this.refreshSubject.subscribe((shouldRefresh) => {
      if (shouldRefresh) {
        this.dataSource.loadPlanEntries();

        this.refreshSubject.unsubscribe();
      }
    });
  }
}
