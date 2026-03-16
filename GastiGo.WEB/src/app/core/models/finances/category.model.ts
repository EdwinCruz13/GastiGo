import { Nature } from "./nature.model";

export interface Category {
  categoryId: string;
  userId: string;
  name: string;
  nature: Nature;
  description: string;
  parentId?: string | null;
  level: number,
  children?: Category[];
}

export interface CategoryRequestDTO {
  userId: string;
  parentId?: string | null;
  name: string;
  description: string;
  natureId: string;
}