export default interface ModalStore {
  isAuth: boolean;
  isLogin: boolean;
  isRegister: boolean;
  isLogout: boolean;
  fromPath: string | undefined;
  setFromPath: (state: string | undefined) => void;
  setAuth: (state: boolean) => void;
  setLogin: (state: boolean) => void;
  setRegister: (state: boolean) => void;
  setLogout: (state: boolean) => void;
  unsetAll: () => void;
}
