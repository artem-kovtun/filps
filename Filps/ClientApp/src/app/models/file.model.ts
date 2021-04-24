import {Storage} from './enums/storage.enum';

export class File {
  id: string;
  name: string;
  content: string;
  source?: Storage;
  path: string;
}
