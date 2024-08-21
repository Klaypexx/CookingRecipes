export default interface ModalStore {
  isAuth: boolean;
  isLogin: boolean;
  isRegister: boolean;
  isLogout: boolean;
  setAuth: (state: boolean) => void;
  setLogin: (state: boolean) => void;
  setRegister: (state: boolean) => void;
  setLogout: (state: boolean) => void;
  unsetAll: () => void;
}
