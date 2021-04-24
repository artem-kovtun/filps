import {Question} from './question.model';
import {ICopyable} from '../interfaces/iCopyable.interface';

export class QuestionAnswer extends Question implements ICopyable {
  answer: string = null;

  copy = () => {
    return {
      text: this.text
    };
  }
}
