export interface TreeNode<T = any> {
  id: string | number;
  label: string;
  level: number;
  type: string;
  data?: T;
  children?: TreeNode<T>[];
}