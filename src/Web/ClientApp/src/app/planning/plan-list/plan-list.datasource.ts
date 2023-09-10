import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { PlanEntryModel } from './../model/plan-entry.model';
import { PlanEntryService } from './../services/plan-entry.service';
import { BehaviorSubject, Observable, catchError, finalize, of, take } from 'rxjs';

export class PlanListDataSource implements DataSource<PlanEntryModel> {
  readonly #planEntryService: PlanEntryService;
  readonly #planEntrySubject = new BehaviorSubject<PlanEntryModel[]>([]);
  readonly #loadingSubject = new BehaviorSubject<boolean>(false);

  public loading$ = this.#loadingSubject.asObservable();

  constructor(planEntryService: PlanEntryService) {
    this.#planEntryService = planEntryService;
  }
  
  public connect(collectionViewer: CollectionViewer): Observable<readonly PlanEntryModel[]> {
    return this.#planEntrySubject.asObservable();
  }

  public disconnect(collectionViewer: CollectionViewer): void {
    this.#planEntrySubject.complete();
    this.#loadingSubject.complete();
  }

  public loadPlanEntries(
    filter: string = '',
    sortDirection: string = 'asc',
    pageIndex: number = 0,
    pageSize: number = 3): void
  {
    this.#loadingSubject.next(true);

    this.#planEntryService
      .findPlans(filter, sortDirection, pageIndex, pageSize)
      .pipe(
        take(1),
        catchError(() => of([])),
        finalize(() => {
          this.#loadingSubject.next(false);
        })
      )
      .subscribe(models => {
        this.#planEntrySubject.next(models);
      })
  }  
}
