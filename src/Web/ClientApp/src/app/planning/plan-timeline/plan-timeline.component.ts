import { Component, OnInit } from '@angular/core';
import { PlanTimelineService } from './../services/plan-timeline.service';
import { take } from 'rxjs';
import { PlanEventWithBalanceModel } from '../model/plan-even-with-balance.model';

@Component({
  selector: 'plan-timeline',
  templateUrl: './plan-timeline.component.html',
  styleUrls: ['./plan-timeline.component.css']
})
export class PlanTimelineComponent implements OnInit {
  readonly #timelineService: PlanTimelineService;

  constructor(timelineService: PlanTimelineService) {
    this.#timelineService = timelineService;
  }

  ngOnInit(): void {
    this.#timelineService.getTimeline(new Date("2030-1-1"))
      .pipe(take(1))
      .subscribe((timeline) => {

        this.graphData = [
          {
            x: timeline.events.map((evt: PlanEventWithBalanceModel) => evt.date),
            y: timeline.events.map((evt: PlanEventWithBalanceModel) => evt.balance),
            type: 'scatter',
            mode: 'lines+points',
            marker: {
              color: 'purple'
            }
          }
        ];
      });
  }

  public graphLayout = {
    autoSize: true,
    title: 'Final Plantasy'
  };

  public graphData: any;
}
