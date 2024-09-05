export default interface UserStore {
  userName: string;
  likesCount: number;
  favouritesCount: number;

  setUserName: (state: string) => void;
  setDefaultLikesCount: (state: number) => void;
  setDefaultFavouritesCount: (state: number) => void;
  setLikesCount: (state: number) => void;
  setFavouritesCount: (state: number) => void;
}
