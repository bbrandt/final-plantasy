import { Component, OnInit } from '@angular/core';
import { PlanEntryService } from './../services/plan-entry.service';
import { PlanListDataSource } from './plan-list.datasource';

@Component({
  selector: 'plan-list',
  templateUrl: './plan-list.component.html',
  styleUrls: ['./plan-list.component.css']
})
export class PlanListComponent implements OnInit {
  readonly #planEntryService: PlanEntryService;
  public dataSource!: PlanListDataSource;

  public displayedColumns = ["id", "eventDate", "amount"];

  constructor(planEntryService: PlanEntryService) {
    this.#planEntryService = planEntryService;
  }

  public ngOnInit(): void {
    this.dataSource = new PlanListDataSource(this.#planEntryService);
    this.dataSource.loadPlanEntries();
  }

  public onRowClicked(row: any) {
    console.log(row);
  }
}
