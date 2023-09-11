import { PlanType } from './plan-type.enum';
import { PlanRepeatOn } from './plan-repeat-on.enum';
import { PersistentState } from './persistent-state.enum';

export interface PlanEntryModel {
  id?: number | undefined;
  eventDate: string;
  planType: PlanType;
  amount: number;
  description: string;
  repeatOn: PlanRepeatOn;
  endDate: string | undefined | null;
  persistentState: PersistentState;
}

