import {Component, Input, OnInit} from '@angular/core';
import {FillMissing} from '../../models/fillMissing.model';
import {FillMissingTypeEnum} from '../../models/fillMissingType.enum';

@Component({
  selector: 'app-fill-missing',
  templateUrl: './fill-missing.component.html',
  styleUrls: ['./fill-missing.component.scss']
})
export class FillMissingComponent implements OnInit {

  FillMissingTypeEnum = FillMissingTypeEnum;
  @Input() fillMissing: FillMissing;
  selectionStart = 0;
  selectionEnd = 0;

  constructor() { }

  ngOnInit(): void {
  }

  insert = (type: FillMissingTypeEnum) => {
    if (type === FillMissingTypeEnum.Input) {
      this.fillMissing.text = [this.fillMissing.text.slice(0, this.selectionStart + 1),
        `|INPUT:1|`, this.fillMissing.text.slice(this.selectionEnd + 1)].join('');
    }
  }

  getSelection = () => {

    const selection = window.getSelection();
    const fulltextIndex = this.fillMissing.text.indexOf(selection.anchorNode.nodeValue);

    this.selectionStart = fulltextIndex + selection.anchorOffset;
    this.selectionEnd = fulltextIndex + selection.focusOffset;
  }

  log = () => console.log(this.fillMissing.text);
}
