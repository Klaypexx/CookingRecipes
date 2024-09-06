export default interface UserStore {
  userName: string;
  nameOfUser: string;
  likesCount: number;
  favouritesCount: number;

  setUserName: (state: string) => void;
  setNameOfUser: (state: string) => void;
  setDefaultLikesCount: (state: number) => void;
  setDefaultFavouritesCount: (state: number) => void;
  setLikesCount: (state: number) => void;
  setFavouritesCount: (state: number) => void;
}
