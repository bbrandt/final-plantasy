import { PlanType } from './plan-type.enum';
import { PlanRepeatOn } from './plan-repeat-on.enum';

export interface PlanEntryModel {
  eventDate: string;
  planType: PlanType;
  amount: number;
  description: string;
  repeatOn: PlanRepeatOn;
}

