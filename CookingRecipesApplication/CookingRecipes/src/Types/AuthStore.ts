export default interface AuthStore {
  isAuthorized: boolean;
  setAuthorized: (state: boolean) => void;
}
