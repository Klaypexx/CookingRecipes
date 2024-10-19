import { useState } from 'react';
import { PORTION_FILTER, TIME_FILTER } from '../../../Constants/recipe';
import useModalStore from '../../../Stores/useModalStore';
import useRecipeStore from '../../../Stores/useRecipeStore';
import FilterValues from '../../../Types/FilterValues';
import BaseButton from '../../Button/BaseButton/BaseButton';
import ButtonBlock from '../../Button/ButtonBlock/ButtonBlock';
import styles from './FilterList.module.css';

const FilterList = () => {
  const { filterString, setFilterString } = useRecipeStore();
  const { unsetAll } = useModalStore();
  const [filter, setFilter] = useState<FilterValues>({
    time: filterString.time,
    portion: filterString.portion,
  });

  const handleOptionClick = (category: keyof FilterValues, option: string) => {
    setFilter((prevFilter) => ({
      ...prevFilter,
      [category]: prevFilter[category] === option ? '' : option,
    }));
  };

  const handleResetClick = () => {
    setFilter({ time: '', portion: '' });
  };

  const handleSearch = () => {
    setFilterString(filter);

    unsetAll();
  };

  return (
    <>
      <div className={styles.filterListBox}>
        <div className={styles.cookingTimeBox}>
          <h4>Время приготовления</h4>
          <div className={styles.optionBox}>
            {Object.entries(TIME_FILTER).map(([key, value]) => (
              <BaseButton
                className={styles.filterButton}
                key={key}
                buttonText={value}
                onClick={() => handleOptionClick('time', key)}
                primary={filter.time === key}
              />
            ))}
          </div>
        </div>
        <div>
          <h4>Количество персон</h4>
          <div className={styles.optionBox}>
            {Object.entries(PORTION_FILTER).map(([key, value]) => (
              <BaseButton
                className={styles.filterButton}
                key={key}
                buttonText={value}
                onClick={() => handleOptionClick('portion', key)}
                primary={filter.portion === key}
              />
            ))}
          </div>
        </div>
        <ButtonBlock
          primaryButtonText="Найти"
          secondaryButtonText="Сбросить"
          primaryType="button"
          onClickPrimary={handleSearch}
          onClickSecondary={handleResetClick}
        />
      </div>
    </>
  );
};

export default FilterList;
