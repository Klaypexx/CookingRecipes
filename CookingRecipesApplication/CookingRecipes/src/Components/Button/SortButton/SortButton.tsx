import { useEffect, useRef, useState } from 'react';
import { RECIPES_SORT_BY } from '../../../Constants/recipe';
import useRecipeStore from '../../../Stores/useRecipeStore';
import BaseButton from '../BaseButton/BaseButton';
import styles from './SortButton.module.css';

const SortButton = () => {
  const [showSelect, setShowSelect] = useState(false);
  const { sortString, setSortString } = useRecipeStore();
  const listBoxRef = useRef<HTMLDivElement | null>(null);

  useEffect(() => {
    // Add event listener for clicks
    document.addEventListener('mousedown', handleClickOutside);

    // Clean up event listener on component unmount
    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, []);

  const toggleSelect = () => {
    setShowSelect((prevShowSelect) => !prevShowSelect);
  };

  const handleClickOutside = (event: MouseEvent) => {
    if (listBoxRef.current && !listBoxRef.current.contains(event.target as Node)) {
      setShowSelect(false);
    }
  };

  const handleItemClick = (key: string, index: number) => {
    console.log(key);
    setSortString(key);
    setShowSelect(false);
  };

  return (
    <div className={styles.sortButtonBox} ref={listBoxRef}>
      <BaseButton
        primary={sortString != '' && sortString != 'Name'}
        buttonText="Сортировка"
        className={styles.paramButton}
        onClick={toggleSelect}
      />
      {showSelect && (
        <div className={styles.listBox}>
          {Object.entries(RECIPES_SORT_BY).map(([key, value], index) => (
            <div
              key={index}
              className={`${styles.listElement} ${sortString === key ? styles.active : ''}`}
              onClick={() => handleItemClick(key, index)}
            >
              <p>{value}</p>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default SortButton;
