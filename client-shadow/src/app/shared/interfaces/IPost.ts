export interface IPost
{
  manga: IMangaInformation,
  author: IPostUserAuthor,

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
  title: string,
  description: string,
  image? : string
}

export interface IComment
{
  name: string,
  avatar: string,
  text: string,
  timestamp: string
}
