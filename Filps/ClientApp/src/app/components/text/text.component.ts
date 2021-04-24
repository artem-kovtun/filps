import {Component, Input, OnInit} from '@angular/core';
import {View} from '../../models/enums/view.enum';
import {Text} from '../../models/text.model';

@Component({
  selector: 'app-text',
  templateUrl: './text.component.html',
  styleUrls: ['./text.component.scss']
})
export class TextComponent implements OnInit {

  View = View;
  @Input() view: View = View.Read;
  @Input() text: Text;

  constructor() { }

  ngOnInit(): void {
  }
}
