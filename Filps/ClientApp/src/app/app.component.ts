import {Component, OnInit} from '@angular/core';
import {FileModel} from './models/file.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  file: FileModel;

  constructor() {}

  ngOnInit(): void {
  }

  isFileSelected = () => this.file != null;

  fileChanged = (file: FileModel) => this.file = file;

  fileClosed = () => this.file = null;
}
