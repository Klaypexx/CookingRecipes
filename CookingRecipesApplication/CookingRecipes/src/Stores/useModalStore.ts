import { create } from "zustand";

interface ModalStore {
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

const useModalStore = create<ModalStore>()((set) => ({
    isAuth: false,
    isLogin: false,
    isRegister: false,
    isLogout: false,
    
    setAuth: () => set((state) => ({isAuth: !state.isAuth})),
    setLogin: () => set((state) => ({isLogin: !state.isLogin})),
    setRegister: () => set((state) => ({isRegister: !state.isRegister})),
    setLogout: () => set((state) => ({isLogout: !state.isLogout})),
    unsetAll: () => set(() => ({
        isAuth: false,
        isLogin: false,
        isRegister: false,
        isLogout: false
    }))

}))

export default useModalStore;