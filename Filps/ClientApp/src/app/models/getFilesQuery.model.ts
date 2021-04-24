import {Storage} from './enums/storage.enum';

export class GetFilesQuery {
  storage: Storage;
  parentId: string;
  search: string;
}
