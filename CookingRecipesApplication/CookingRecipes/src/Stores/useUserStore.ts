import { create } from 'zustand';
import UserStore from '../Types/UserStore';

const useUserStore = create<UserStore>()((set) => ({
  userName: '',

  setUserName: (state) => set({ userName: state }),
}));

export default useUserStore;
