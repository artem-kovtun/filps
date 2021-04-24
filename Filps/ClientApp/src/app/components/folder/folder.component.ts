import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {StorageObject} from '../../models/storageObject.model';

@Component({
  selector: 'app-folder',
  templateUrl: './folder.component.html',
  styleUrls: ['./folder.component.scss']
})
export class FolderComponent implements OnInit {

  @Input() folder: StorageObject;
  @Output() onClick = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }

  onFolderClick = () => this.onClick.emit(this.folder.id);
}
