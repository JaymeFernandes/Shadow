export interface IUpdateDay
{
  title : string;

  updates: IRelease[];
}

export interface IRelease
{
  title: string;
  cover: string;

  isNew: boolean;
  cap : Number;
}
