import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter, signal } from '@angular/core';
import { TreeNode } from '@core/models/common/tree-node.model';


@Component({
  selector: 'app-tree-node',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './tree-node.component.html',
})
/**
 * Componente genérico para representar un nodo en un árbol. Recibe un nodo de tipo TreeNode<T> y emite eventos para agregar, editar o eliminar nodos.
 * @template T - El tipo de datos que representa el nodo.
 */
export class TreeNodeComponent<T>  {

  @Input() node!: TreeNode<T>;

  @Output() add = new EventEmitter<TreeNode<T>>();
  @Output() edit = new EventEmitter<TreeNode<T>>();
  @Output() delete = new EventEmitter<TreeNode<T>>();

  selectedNode = signal<TreeNode | null>(null);
  expanded = signal(false);

  // Alterna el estado de expansión del nodo para mostrar u ocultar sus hijos.
  selectNode(node: TreeNode) {
    this.selectedNode.set(node);
    this.expanded.update(v => !v);
  }


}