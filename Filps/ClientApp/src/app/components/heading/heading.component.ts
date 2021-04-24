import {Component, Input, OnInit} from '@angular/core';
import {View} from '../../models/enums/view.enum';
import {Heading} from '../../models/heading.model';

@Component({
  selector: 'app-heading',
  templateUrl: './heading.component.html',
  styleUrls: ['./heading.component.scss']
})
export class HeadingComponent implements OnInit {

  View = View;
  @Input() heading: Heading;
  @Input() view = View.Read;

  constructor() { }

  ngOnInit(): void {
  }

}
