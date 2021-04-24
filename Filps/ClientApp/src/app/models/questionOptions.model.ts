import {Question} from './question.model';
import {Option} from './option.model';

export class QuestionOptions extends Question {
  isMultipleCorrect = false;
  options: Array<Option> = [new Option()];
  selectedOptions: Array<Option> = [];
}
