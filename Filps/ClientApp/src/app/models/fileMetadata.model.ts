import {Storage} from './enums/storage.enum';

export class FileMetadata {
  source?: Storage;
  id: string;
  name: string;
  isPinned: boolean;
  createdOn: Date;
}
