import { DashboardCategory, DashboardGroup } from "@core/models/dashboard/dashboard.model";

export const MONTHS = [
  'Ene', 'Feb', 'Mar', 'Abr',
  'May', 'Jun', 'Jul', 'Ago',
  'Sep', 'Oct', 'Nov', 'Dic'
];


export function getCategoryTotal(category: DashboardCategory): number {
  return category.values.reduce((acc, v) => acc + v.amount, 0);
}

export function getGroupTotal(group: DashboardGroup): number {
  return group.categories.reduce((acc, cat) => {
    return acc + getCategoryTotal(cat);
  }, 0);
}
