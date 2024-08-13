import { create } from 'zustand';
import AuthStore from '../Types/AuthStore';

const useAuthStore = create<AuthStore>()((set) => ({
  isAuthorized: false,

  setAuthorized: (state) => set({ isAuthorized: state }),
}));

export default useAuthStore;
