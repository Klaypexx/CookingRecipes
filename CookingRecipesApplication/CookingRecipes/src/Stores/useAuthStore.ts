import { create } from 'zustand';
import AuthStore from '../Types/AuthStore';

const useAuthStore = create<AuthStore>()((set) => ({
  userName: '',

  setUserName: (newUserName) => set({ userName: newUserName }),
}));

export default useAuthStore;
