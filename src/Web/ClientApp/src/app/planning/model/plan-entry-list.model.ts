import { PlanEntryModel } from './plan-entry.model';

export interface PlanEntryListModel extends PlanEntryModel {
  planTypeName: string;
  repeatOnName: string;
}

