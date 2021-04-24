import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {StorageObject} from '../../models/storageObject.model';

@Component({
  selector: 'app-folders',
  templateUrl: './folders.component.html',
  styleUrls: ['./folders.component.scss']
})
export class FoldersComponent implements OnInit {

  @Input() folders: Array<StorageObject> = [];
  @Output() folderClicked = new EventEmitter<string>() ;

  constructor() { }

  ngOnInit(): void {
  }

  onFolderClick = (id: string) => this.folderClicked.emit(id);

}
