import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FileMetadata} from '../../models/fileMetadata.model';
import {Storage} from '../../models/enums/storage.enum';

@Component({
  selector: 'app-documents',
  templateUrl: './documents.component.html',
  styleUrls: ['./documents.component.scss']
})
export class DocumentsComponent implements OnInit {

  Storage = Storage;

  @Input() files: Array<FileMetadata> = [];
  @Input() allowPin = false;
  @Input() displayDate = false;
  @Output() fileClicked = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }

  onFileClick = (id: string) => this.fileClicked.emit(id);

}
