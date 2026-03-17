export interface TreeNode<T = any> {
  id: string | number;
  label: string;
  level: number;
  type: string;
  isActive?: boolean;
  data?: T;
  children?: TreeNode<T>[];
}
