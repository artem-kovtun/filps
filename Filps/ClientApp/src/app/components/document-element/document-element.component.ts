import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {View} from '../../models/enums/view.enum';

@Component({
  selector: ' app-document-element',
  templateUrl: './document-element.component.html',
  styleUrls: ['./document-element.component.scss']
})
export class DocumentElementComponent implements OnInit {

  View = View;
  @Input() view = View.Read;
  @Input() index: number;
  @Output() onCopy = new EventEmitter<number>();
  @Output() onDelete = new EventEmitter<number>();
  @Output() onAddAfter = new EventEmitter<number>();

  constructor() { }

  ngOnInit(): void {
  }


  copyElement = () => this.onCopy.emit(this.index);
  deleteElement = () => this.onDelete.emit(this.index);
  addAfterElement = () => this.onAddAfter.emit(this.index);

}
