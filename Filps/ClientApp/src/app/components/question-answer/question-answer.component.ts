import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {QuestionAnswer} from '../../models/questionAnswer.model';
import {View} from '../../models/enums/view.enum';
import {QuestionType} from '../../models/enums/questionType.enum';

@Component({
  selector: 'app-question-answer',
  templateUrl: './question-answer.component.html',
  styleUrls: ['./question-answer.component.scss']
})
export class QuestionAnswerComponent implements OnInit {

  View = View;
  QuestionType = QuestionType;
  @Input() view: View = View.Read;
  @Output() questionAnswerChange = new EventEmitter<QuestionAnswer>();
  @Input() questionAnswer: QuestionAnswer;

  constructor() { }

  ngOnInit(): void {}

  onDataChange = () => this.questionAnswerChange.emit(this.questionAnswer);

}
