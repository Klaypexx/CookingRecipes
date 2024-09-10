import classNames from 'classnames';
import useSearchStore from '../../../Stores/useSearchStore';
import MiniTagsListProps from '../../../Types/MiniTagsListProps';
import styles from './MiniTagsList.module.css';

const MiniTagsList: React.FC<MiniTagsListProps> = ({ className, values }) => {
  const { setSearchString } = useSearchStore();

  const handleClick = (value: string) => {
    setSearchString(value);
  };

  return (
    <div className={classNames(styles.miniTagsListBox, className)}>
      {values?.map((value, index) => (
        <div key={index}>
          <p className={styles.miniTagText} onClick={() => handleClick(value)}>
            {value}
          </p>
        </div>
      ))}
    </div>
  );
};

export default MiniTagsList;
