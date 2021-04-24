import { Component, OnInit } from '@angular/core';
import {MatDialogRef} from '@angular/material/dialog';
import {FileElementType} from '../../models/enums/documentElementType.enum';
import {TranslateService} from '@ngx-translate/core';

@Component({
  selector: 'app-section-pick',
  templateUrl: './section-pick.component.html',
  styleUrls: ['./section-pick.component.scss']
})
export class SectionPickComponent implements OnInit {

  sections = [
    { name: this.translate.instant('heading'), value: FileElementType.Heading, image: 'heading' },
    { name: this.translate.instant('text'), value: FileElementType.Text, image: 'text' },
    { name: this.translate.instant('questionAnswer'), value: FileElementType.QuestionAnswer, image: 'question' },
    { name: this.translate.instant('questionOptions'), value: FileElementType.QuestionOptions, image: 'options' },
    { name: this.translate.instant('youtubeLink'), value: FileElementType.YoutubeLink, image: 'youtube' },
    { name: this.translate.instant('audio'), value: FileElementType.Audio, image: 'audio' }
  ];

  constructor(public dialogRef: MatDialogRef<SectionPickComponent>,
              private translate: TranslateService) { }

  ngOnInit(): void {
  }

  selected = (value: FileElementType) => {
    this.dialogRef.close(value);
  }
}
