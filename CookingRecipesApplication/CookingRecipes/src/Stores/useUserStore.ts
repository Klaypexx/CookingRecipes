import { create } from 'zustand';
import UserStore from '../Types/UserStore';

const useUserStore = create<UserStore>()((set) => ({
  isUserUpdate: false,

  setUserUpdate: () => set((state) => ({ isUserUpdate: !state.isUserUpdate })),
}));

export default useUserStore;
