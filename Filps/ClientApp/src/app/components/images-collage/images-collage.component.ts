import {Component, Input, OnInit} from '@angular/core';
import {ImagesCollageModel} from '../../models/imagesCollage.model';

@Component({
  selector: 'app-images-collage',
  templateUrl: './images-collage.component.html',
  styleUrls: ['./images-collage.component.scss']
})
export class ImagesCollageComponent implements OnInit {

  @Input() images: ImagesCollageModel;

  constructor() { }

  ngOnInit(): void {
  }

}
