export class GetFilesQuery {

  constructor(page: number, take: number) {
    this.page = page;
    this.take = take;
  }

  page: number;
  take: number;
  search: string;
}
