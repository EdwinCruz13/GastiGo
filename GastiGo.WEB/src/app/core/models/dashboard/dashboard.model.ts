export interface DashboardYear {
  year: number;
  groups: DashboardGroup[];
}


export interface DashboardGroup {
  name: string; // Income, Expenses, Investment
  categories: DashboardCategory[];
}

export interface DashboardCategory {
  name: string;
  values: MonthlyValue[];
}

export interface MonthlyValue {
  month: number;   // 1 - 12
  amount: number;
}
