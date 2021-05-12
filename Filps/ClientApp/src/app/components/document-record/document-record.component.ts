import {Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {FileMetadata} from '../../models/fileMetadata.model';

@Component({
  selector: 'app-document-record',
  templateUrl: './document-record.component.html',
  styleUrls: ['./document-record.component.scss']
})
export class DocumentRecordComponent implements OnInit {

  @ViewChild('optionsButton') optionsButton;
  @ViewChild('unpinButton') unpinButton;

  @Input() file = new FileMetadata();
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

  optionDropdownVisibleChange = (isVisible: boolean) => {
    if (isVisible) {
      this.optionsButton.nativeElement.className = 'explicitly-visible-button';
      this.unpinButton.nativeElement.className = 'explicitly-visible-button';
    }
    else {
      this.optionsButton.nativeElement.className = null;
      this.unpinButton.nativeElement.className = null;
    }
  }
}
