export interface IPost
{
  manga? : IMangaInformation,
  author: IPostUserAuthor,

  description: string,

  likes: number,
  userHasLiked: boolean,
  userHasFavorited: boolean,

  comments: IComment[]
}

export interface IMangaInformation
{
  title: string,
  author: string,
  coverImage: string,
  rating: number,
}

export interface IPostUserAuthor
{
  name: string,
  avatar: string,
  image? : string
}

export interface IComment
{
  name: string,
  avatar: string,
  text: string,
  timestamp: string
}
