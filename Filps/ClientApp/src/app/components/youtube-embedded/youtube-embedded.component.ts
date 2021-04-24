import {AfterViewInit, Component, ElementRef, Input, OnInit, ViewChild} from '@angular/core';
import {DomSanitizer, SafeResourceUrl} from '@angular/platform-browser';
import {View} from '../../models/enums/view.enum';
import {YoutubeEmbedded} from '../../models/youtube-embedded.model';

@Component({
  selector: 'app-youtube-embedded',
  templateUrl: './youtube-embedded.component.html',
  styleUrls: ['./youtube-embedded.component.scss']
})
export class YoutubeEmbeddedComponent implements OnInit  {

  View = View;
  @Input() youtubeEmbedded: YoutubeEmbedded;
  @Input() view = View.Read;
  embedLink: SafeResourceUrl;
  @ViewChild('frame') frameElement: ElementRef;
  frameHeight = 0;

  constructor(private domSanitizer: DomSanitizer) {}

  ngOnInit(): void {
    this.buildEmbedLink();
  }

  buildEmbedLink = () => {
    if (this.youtubeEmbedded.link !== null) {
      const matches = this.youtubeEmbedded.link.match(/https:\/\/www\.youtube\.com\/watch\?v=(.{11})/);
      this.embedLink = this.domSanitizer.bypassSecurityTrustResourceUrl(`https://www.youtube.com/embed/${matches[1]}`);
    }
  }

}
