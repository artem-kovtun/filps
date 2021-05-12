import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {View} from '../../models/enums/view.enum';
import {QuestionOptions} from '../../models/questionOptions.model';
import {Option} from '../../models/option.model';

@Component({
  selector: 'app-question-options',
  templateUrl: './question-options.component.html',
  styleUrls: ['./question-options.component.scss']
})
export class QuestionOptionsComponent implements OnInit {

  View = View;
  @Input() view: View = View.Read;
  @Output() questionOptionsChange = new EventEmitter<QuestionOptions>();
  @Input() questionOptions: QuestionOptions;

  constructor() { }

  ngOnInit(): void {
  }

  isChecked = (option: Option) => {
    if (this.questionOptions.selectedOptions !== null) {
      return this.questionOptions.selectedOptions.map(o => o.text).includes(option.text);
    }
  }

  saveSelectedOptions = (options: Option[]) => {
    this.questionOptions.selectedOptions = options;
    this.questionOptionsChange.emit(this.questionOptions);
  }

  clearSelection = () => {
    this.questionOptions.selectedOptions = null;
    this.questionOptionsChange.emit(this.questionOptions);
  };

  isClearSelectionButtonVisible = () => this.questionOptions.selectedOptions !== null;

  addOption = () => this.questionOptions.options.push({ text: null });

  deleteOption = (index: number) => this.questionOptions.options.splice(index, 1);

  onDataChange = () => this.questionOptionsChange.emit(this.questionOptions);
}
