import {Component, OnInit} from '@angular/core';
import {File} from './models/file.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  file: File;

  constructor() {}

  ngOnInit(): void {
  }

  isFileSelected = () => this.file != null;

  fileChanged = (file: File) => this.file = file;

  fileClosed = () => this.file = null;
}
