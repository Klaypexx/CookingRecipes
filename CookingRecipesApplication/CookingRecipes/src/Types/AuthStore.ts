export default interface AuthStore {
  userName: string;
  setUserName: (state: string) => void;
}
