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
  @Output() filePinToggle = new EventEmitter<string>();
  @Output() fileDelete = new EventEmitter<string>();
  @Output() fileDownload = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }

  onFileClick = (id: string) => this.fileClicked.emit(id);

  onFilePinToggle = (id: string) => this.filePinToggle.emit(id);

  onFileDelete = (id: string) => this.fileDelete.emit(id);

  onFileDownload = (id: string) => this.fileDownload.emit(id);

}
