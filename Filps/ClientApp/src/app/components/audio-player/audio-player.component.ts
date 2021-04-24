import {Component, Input, OnInit, EventEmitter, Output} from '@angular/core';
import {AudioRecord} from '../../models/audioRecord.model';
import {View} from '../../models/enums/view.enum';

@Component({
  selector: 'app-audio-player',
  templateUrl: './audio-player.component.html',
  styleUrls: ['./audio-player.component.scss']
})
export class AudioPlayerComponent implements OnInit {

  View = View;
  playlist = [];
  @Input() audioRecord: AudioRecord;
  @Input() view = View.Read;
  @Output() onUpload = new EventEmitter<{ audio: AudioRecord, file: File}>();

  constructor() { }

  ngOnInit(): void {
    this.playlist.push(this.audioRecord);
  }

  fileSelected = (file: File) => {
    this.onUpload.emit({audio: this.audioRecord, file});
  }

}
