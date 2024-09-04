export default interface UserStore {
  userName: string;
  setUserName: (state: string) => void;
}
