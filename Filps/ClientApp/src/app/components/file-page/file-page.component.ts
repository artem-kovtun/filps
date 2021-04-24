import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {View} from '../../models/enums/view.enum';
import {File} from '../../models/file.model';
import {FileElement} from '../../models/fileElement.model';
import {MatDialog} from '@angular/material/dialog';
import {FileLocationPickerModalComponent} from '../file-location-picker-modal/file-location-picker-modal.component';
import {Storage} from '../../models/enums/storage.enum';
import {ConfirmModalComponent} from '../confirm-modal/confirm-modal.component';
import {ConfirmModalType} from '../../models/enums/confirmModalType.enum';

@Component({
  selector: 'app-file-page',
  templateUrl: './file-page.component.html',
  styleUrls: ['./file-page.component.scss']
})
export class FilePageComponent implements OnInit {

  @Input() file: File;
  @Output() closeFile = new EventEmitter();

  View = View;
  fileView = View.Read;
  Storage = Storage;

  filenameEdit: string;
  fileContent: Array<FileElement>;
  fileContentCopy: Array<FileElement>;
  drive: { storage: Storage, provider: string, icon: string };
  drives = [
    {
      storage: Storage.GoogleDrive,
      provider: 'Google',
      icon: 'google'
    },
    {
      storage: Storage.OneDrive,
      provider: 'Microsoft',
      icon: 'windows'
    },
    {
      storage: Storage.Dropbox,
      provider: 'Dropbox',
      icon: 'dropbox'
    }
  ];

  constructor(public dialog: MatDialog) { }

  ngOnInit(): void {
    if (this.file.name == null) {
      this.fileView = View.Edit;
    }
    else {
      this.filenameEdit = this.file.name;
    }
    if (this.file.source != null) {
      this.drive = this.drives.filter(d => d.storage === this.file.source)[0];
    }

    this.fileContent = (this.file.content != null ? JSON.parse(this.file.content) : []);
  }

  onFileEdit = () => {
    this.fileContentCopy = Object.assign({}, this.fileContent);
    this.fileView = View.Edit;
  }

  closeDocument = () => {
    const dialog = this.dialog.open(ConfirmModalComponent, {
      width: '450px',
      backdropClass: 'explicit-overlay-dark-backdrop',
      data: {
        message: 'There is unsaved changes in your file. Do you really want to close it?',
        type: ConfirmModalType.Primary,
        confirmMessage: 'Yes, proceed',
        cancelMessage: 'No, stay'
      }
    });

    dialog.afterClosed().subscribe(confirm => {
      if (confirm) {
        this.closeFile.emit();
      }
    });
  }

  visibleChange = (isVisible: boolean) => {

    if (isVisible) {
      this.filenameEdit = this.file.name;
    }
    else {
      if (this.filenameEdit !== '') {
        this.file.name = this.filenameEdit;
      }
    }
  }

  cancelEdit = () => {
    if (this.file.name == null) {
      this.closeFile.emit();
      return;
    }
    this.fileView = View.Read;
  }

  saveEdit = () => {
    if (this.file.name == null) {
      const dialog = this.dialog.open(FileLocationPickerModalComponent, {
        width: '800px',
        backdropClass: 'explicit-overlay-dark-backdrop'
      });

      dialog.afterClosed().subscribe(result => {
        if (result.isSelected) {
          alert('Save');
          this.fileView = View.Read;
        }
      });
    }
    else {
      // call service
      this.fileView = View.Read;
    }
  }

}
