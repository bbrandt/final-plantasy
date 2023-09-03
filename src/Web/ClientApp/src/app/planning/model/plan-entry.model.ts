import { PlanType } from './plan-type.enum';

export interface PlanEntryModel {
  eventDate: string;
  planType: PlanType;
  amount: number;
}

