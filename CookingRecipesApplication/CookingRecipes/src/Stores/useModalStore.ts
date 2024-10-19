import { create } from 'zustand';
import ModalStore from '../Types/ModalStore';

const useModalStore = create<ModalStore>()((set) => ({
  isAuth: false,
  isLogin: false,
  isRegister: false,
  isLogout: false,
  isFilter: false,

  setAuth: () => set((state) => ({ isAuth: !state.isAuth })),
  setLogin: () => set((state) => ({ isLogin: !state.isLogin })),
  setRegister: () => set((state) => ({ isRegister: !state.isRegister })),
  setLogout: () => set((state) => ({ isLogout: !state.isLogout })),
  setFilter: () => set((state) => ({ isFilter: !state.isFilter })),
  unsetAll: () =>
    set(() => ({
      isAuth: false,
      isLogin: false,
      isRegister: false,
      isLogout: false,
      isFilter: false,
    })),
}));

export default useModalStore;
