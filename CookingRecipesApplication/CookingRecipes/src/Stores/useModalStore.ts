import { create } from 'zustand';
import ModalStore from '../Types/ModalStore';

const useModalStore = create<ModalStore>()((set) => ({
  isAuth: false,
  isLogin: false,
  isRegister: false,
  isLogout: false,
  fromPath: undefined,

  setFromPath: (state) => set({ fromPath: state }),
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
  set,
}));

export default useModalStore;
