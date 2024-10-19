import FilterList from '../../Filter/FilterList/FilterList';
import BaseModal from '../BaseModal/BaseModal';

const FilterModal = () => {
  return (
    <>
      <BaseModal haederText="Фильтрация">
        <FilterList />
      </BaseModal>
    </>
  );
};

export default FilterModal;
