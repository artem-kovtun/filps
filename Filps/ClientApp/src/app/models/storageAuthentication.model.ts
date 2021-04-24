import {Storage} from './enums/storage.enum';

export class StorageAuthentication {
  storage: Storage;
  isAuthorized: boolean;
  isLoaded = false;
}
