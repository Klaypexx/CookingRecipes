import { useEffect, useRef, useState } from 'react';
import { RECIPES_SORT_BY } from '../../../Constants/recipe';
import BaseButton from '../BaseButton/BaseButton';
import styles from './SortButton.module.css';

const SortButton = () => {
  const [showSelect, setShowSelect] = useState(false);
  const [activeIndex, setActiveIndex] = useState<number | null>(0);
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

  const handleItemClick = (item: string, index: number) => {
    console.log(item);
    setActiveIndex(index);
    setShowSelect(false);
  };

  return (
    <div className={styles.sortButtonBox} ref={listBoxRef}>
      <BaseButton buttonText="Сортировать" className={styles.paramButton} onClick={toggleSelect} />
      {showSelect && (
        <div className={styles.listBox}>
          {RECIPES_SORT_BY.map((item, index) => (
            <div
              key={index}
              className={`${styles.listElement} ${activeIndex === index ? styles.active : ''}`}
              onClick={() => handleItemClick(item, index)}
            >
              <p>{item}</p>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default SortButton;
