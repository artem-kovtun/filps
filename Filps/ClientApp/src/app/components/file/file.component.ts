import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FileElementType} from '../../models/enums/documentElementType.enum';
import {View} from '../../models/enums/view.enum';
import {CdkDragDrop, moveItemInArray, transferArrayItem} from '@angular/cdk/drag-drop';
import {FileElement} from '../../models/fileElement.model';
import {MatDialog} from '@angular/material/dialog';
import {SectionPickComponent} from '../section-pick/section-pick.component';
import {QuestionAnswer} from '../../models/questionAnswer.model';
import {QuestionOptions} from '../../models/questionOptions.model';
import {Heading} from '../../models/heading.model';
import {YoutubeEmbedded} from '../../models/youtube-embedded.model';
import {AudioRecord} from '../../models/audioRecord.model';
import {ConfirmModalComponent} from '../confirm-modal/confirm-modal.component';
import {ConfirmModalType} from '../../models/enums/confirmModalType.enum';

@Component({
  selector: 'app-file',
  templateUrl: './file.component.html',
  styleUrls: ['./file.component.scss']
})
export class FileComponent implements OnInit {

  FileElementType = FileElementType;
  View = View;

  @Input() fileContent = new Array<FileElement>();
  @Output() fileContentChange = new EventEmitter<Array<FileElement>>();
  @Input() view = View.Read;

  constructor(public dialog: MatDialog) { }

  ngOnInit(): void {
  }

  drop = (event: CdkDragDrop<FileElement[]>) => {
    if (event.previousContainer === event.container) {
      moveItemInArray(this.fileContent, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(this.fileContent,
        event.container.data,
        event.previousIndex,
        event.currentIndex);
    }
    this.fileContentChange.emit(this.fileContent);
  }

  deleteFileElement = (index: number) => {
    const dialog = this.dialog.open(ConfirmModalComponent, {
      width: '350px',
      backdropClass: 'explicit-overlay-dark-backdrop',
      data: {
        message: 'Do you really want to delete this item?',
        type: ConfirmModalType.Danger,
        confirmMessage: 'Yes, delete',
        cancelMessage: 'No, keep'
      }
    });

    dialog.afterClosed().subscribe(isConfirmed => {
      if (isConfirmed) {
        this.fileContent = this.fileContent.filter((_, i) => i !== index);
        this.fileContentChange.emit(this.fileContent);
      }
    });
  }

  copyFileElement = (index: number) => {
    const fileElement = this.fileContent[index];
    this.fileContent.splice(index, 0,  {
      type: fileElement.type,
      model: Object.assign({}, fileElement.model)
    });
    this.fileContentChange.emit(this.fileContent);
  }

  addSection = () => {
    const dialog = this.dialog.open(SectionPickComponent, {
      width: '350px',
      backdropClass: 'explicit-overlay-dark-backdrop'
    });

    dialog.afterClosed().subscribe(value => {
      if (value != null) {
        this.addNewSectionToFile(this.fileContent.length, value);
        this.fileContentChange.emit(this.fileContent);
      }
    });
  }

  addAfterElement = (index: number) => {
    const dialog = this.dialog.open(SectionPickComponent, {
      width: '350px',
      backdropClass: 'explicit-overlay-dark-backdrop'
    });

    dialog.afterClosed().subscribe(value => {
      if (value != null) {
        this.addNewSectionToFile(index + 1, value);
        this.fileContentChange.emit(this.fileContent);
      }
    });
  }

  addNewSectionToFile = (index: number, type: FileElementType) => {
    const typeModelMap = new Map<FileElementType, any>([
      [FileElementType.QuestionAnswer, new QuestionAnswer()],
      [FileElementType.QuestionOptions, new QuestionOptions()],
      [FileElementType.Text, new Text()],
      [FileElementType.Heading, new Heading()],
      [FileElementType.YoutubeLink, new YoutubeEmbedded()],
      [FileElementType.Audio, new AudioRecord()]
    ]);

    this.fileContent.splice(index, 0, { type, model: typeModelMap.get(type) });
  }

  fileElementChange = () => {
    this.fileContentChange.emit(this.fileContent);
  }
}
