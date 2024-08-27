import { create } from 'zustand';
import SearchStore from '../Types/SearchStore';

const useSearchStore = create<SearchStore>()((set) => ({
  searchString: '',

  setSearchString: (state) => set({ searchString: state }),
}));

export default useSearchStore;
