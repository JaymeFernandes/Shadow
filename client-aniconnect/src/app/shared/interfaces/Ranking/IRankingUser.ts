export interface IRankingUser
{
  user: IUserInformation
  nivel: Number,
  trophy: Number,
  exp: Number
}

export interface IUserInformation
{
  nick: string,
  name: string,
  avatar: string
}
