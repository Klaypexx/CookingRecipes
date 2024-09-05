import { create } from 'zustand';
import UserStore from '../Types/UserStore';

const useUserStore = create<UserStore>()((set) => ({
  userName: '',
  likesCount: 0,
  favouritesCount: 0,

  setUserName: (state) => set({ userName: state }),
  setDefaultFavouritesCount: (state) => set({ favouritesCount: state }),
  setDefaultLikesCount: (state) => set({ likesCount: state }),
  setLikesCount: (values) => set((state) => ({ likesCount: state.likesCount + values })),
  setFavouritesCount: (values) => set((state) => ({ favouritesCount: state.favouritesCount + values })),
}));

export default useUserStore;
