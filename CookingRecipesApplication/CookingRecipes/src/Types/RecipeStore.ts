export default interface RecipeStore {
  searchString: string;
  sortString: string;
  filterString: { time: string; portion: string };
  setSearchString: (state: string) => void;
  setSortString: (state: string) => void;
  setFilterString: (state: { time: string; portion: string }) => void;
}
