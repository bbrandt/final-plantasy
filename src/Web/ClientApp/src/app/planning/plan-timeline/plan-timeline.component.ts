import { Component, OnInit } from '@angular/core';
import { DateTime } from 'luxon';
import { PlanTimelineService } from './../services/plan-timeline.service';
import { take } from 'rxjs';
import { PlanEventWithBalanceModel } from './../model/plan-even-with-balance.model';
import { PlanTimelineModel } from './../model/plan-timeline.model';

@Component({
  selector: 'plan-timeline',
  templateUrl: './plan-timeline.component.html',
  styleUrls: ['./plan-timeline.component.css']
})
export class PlanTimelineComponent implements OnInit {
    readonly #timelineService: PlanTimelineService;
    #timeline?: PlanTimelineModel;

    public graphLayout = {
        autoSize: true,
        yaxis: {
            rangemode: 'nonnegative'
        }
    };

    public graphData: any;

    constructor(timelineService: PlanTimelineService) {
        this.#timelineService = timelineService;
    }

    ngOnInit(): void {
        this.#timelineService.getTimeline(new Date("2030-1-1"))
            .pipe(take(1))
            .subscribe((timeline) => {
                this.#timeline = timeline;

                this.graphData = this.createGraphData(timeline);
            });
    }

    private createGraphData(timeline: PlanTimelineModel): Array<any> {
        const data = [{
            x: timeline.events.map((evt: PlanEventWithBalanceModel) => evt.date),
            y: timeline.events.map((evt: PlanEventWithBalanceModel) => evt.balance),
            type: 'scatter',
            mode: 'lines+points',
            marker: {
                color: 'purple'
            }
        }];

        return data;
    }

    public getRemainingTime(): string {
        if (!this.#timeline || this.#timeline.events.length <= 1) {
            return "";
        }

        const end = this.#timeline.events
            .find((item) => {
                return item.balance <= 0;
            });

        const start = this.#timeline.events[0];

        if (!end || !start) {
            return "";
        }

        const endDate = DateTime.fromISO(end.date);
        const startDate = DateTime.fromISO(start.date)

        const difference = endDate.diff(startDate, ["years"]);

        const displayYears = difference.years.toFixed(2);

        return `${displayYears} year(s) to zero`;
    }
}
