import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {View} from '../../models/enums/view.enum';
import {FileModel} from '../../models/file.model';
import {FileElement} from '../../models/fileElement.model';
import {MatDialog} from '@angular/material/dialog';
import {FileLocationPickerModalComponent} from '../file-location-picker-modal/file-location-picker-modal.component';
import {Storage} from '../../models/enums/storage.enum';
import {ConfirmModalComponent} from '../confirm-modal/confirm-modal.component';
import {ConfirmModalType} from '../../models/enums/confirmModalType.enum';
import {FileService} from '../../services/file.service';
import {saveAs} from 'file-saver';
import {AuthService} from '../../services/auth.service';

@Component({
  selector: 'app-file-page',
  templateUrl: './file-page.component.html',
  styleUrls: ['./file-page.component.scss']
})
export class FilePageComponent implements OnInit {

  @Input() file: FileModel;
  @Output() closeFile = new EventEmitter();

  View = View;
  fileView = View.Read;
  Storage = Storage;

  filenameEdit: string;
  fileContent: Array<FileElement>;
  initialFileContent: Array<FileElement>;
  initialFilename: string;

  isFirstTimeCreate = false;
  isFileModified = false;
  isFilenameModified = false;

  drivesNames = {
    1: 'Google',
    2: 'Microsoft',
    3: 'Dropbox',
    4: 'Filps',
  };

  constructor(public dialog: MatDialog,
              private fileService: FileService,
              private authService: AuthService) { }

  ngOnInit(): void {
    if (this.file.name == null) {
      this.fileView = View.Edit;
      this.isFirstTimeCreate = !this.isFirstTimeCreate;
    }
    else {
      this.filenameEdit = this.file.name;
      this.initialFilename = this.file.name;
    }

    this.fileContent = (this.file.serializedContent != null ? JSON.parse(this.file.serializedContent) : []);
    this.initialFileContent = (this.file.serializedContent != null ? JSON.parse(this.file.serializedContent) : []);
  }

  onFileEdit = () => {
    this.fileView = View.Edit;
  }

  closeDocument = () => {
    if (this.isSaveButtonHighlighted()) {
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
    else {
      this.closeFile.emit();
    }
  }

  visibleChange = (isVisible: boolean) => {

    if (isVisible) {
      this.filenameEdit = this.file.name;
    }
    else {
      if (this.filenameEdit !== '') {
        this.file.name = this.filenameEdit;
        this.isFilenameModified = this.file.name !== this.initialFilename;
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

  isSaveButtonHighlighted = () => {
    return this.file.id == null || this.isFileModified || this.isFilenameModified;
  }

  save = () => {
    if (this.file.name == null || !this.authService.isAuthorized()) {
      const dialog = this.dialog.open(FileLocationPickerModalComponent, {
        width: '400px',
        backdropClass: 'explicit-overlay-dark-backdrop',
        data: {
          name: this.file.name,
          path: this.file.path
        }
      });

      dialog.afterClosed().subscribe(result => {
        if (result.isSelected) {
          this.file.name = result.file.name;
          this.saveFile();
        }
      });
    }
    else {
      this.saveFile();
    }
  }

  saveFile = () => {
    this.file.serializedContent = JSON.stringify(this.fileContent);
    this.fileService.save(this.file).subscribe(result => {
      this.fileService.get(result.id).subscribe(file => {
        this.file = file;
        this.fileContent = JSON.parse(file.serializedContent);
        this.initialFileContent = JSON.parse(file.serializedContent);
        this.initialFilename = file.name;
        this.fileView = View.Read;
        this.isFirstTimeCreate = false;
        this.isFileModified = false;
        this.isFilenameModified = false;
      });
    });
  }

  download = () => {
    this.fileService.download(this.file.id).subscribe(data => {
      saveAs(data, this.file.name   + '.idf');
    });
  }

  onFileChange = (fileContent: Array<FileElement>) => {
    this.isFileModified = JSON.stringify(this.initialFileContent) !== JSON.stringify(fileContent);
  }
}
