import {Question} from './question.model';
import {ICopyable} from '../interfaces/iCopyable.interface';
import {QuestionType} from './enums/questionType.enum';

export class QuestionAnswer extends Question implements ICopyable {
  answer = '';
  type = QuestionType.Text;

  copy = () => {
    return {
      text: this.text
    };
  }
}
