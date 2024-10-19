import { create } from 'zustand';
import RecipeStore from '../Types/RecipeStore';

const useRecipeStore = create<RecipeStore>()((set) => ({
  searchString: '',
  sortString: '',
  filterString: {
    time: '',
    portion: '',
  },

  setSearchString: (state) => set({ searchString: state }),
  setSortString: (state) => set({ sortString: state }),
  setFilterString: (state) => set({ filterString: { time: state.time, portion: state.portion } }),
}));

export default useRecipeStore;
