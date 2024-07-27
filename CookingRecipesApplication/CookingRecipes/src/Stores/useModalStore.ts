import { create } from 'zustand';
import { ModalStore } from '../Types/types';

const useModalStore = create<ModalStore>()((set) => ({
  isAuth: false,
  isLogin: false,
  isRegister: false,
  isLogout: false,

  setAuth: () => set((state) => ({ isAuth: !state.isAuth })),
  setLogin: () => set((state) => ({ isLogin: !state.isLogin })),
  setRegister: () => set((state) => ({ isRegister: !state.isRegister })),
  setLogout: () => set((state) => ({ isLogout: !state.isLogout })),
  unsetAll: () =>
    set(() => ({
      isAuth: false,
      isLogin: false,
      isRegister: false,
      isLogout: false,
    })),
}));

export default useModalStore;
