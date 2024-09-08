import classNames from 'classnames';
import useSearchStore from '../../../Stores/useSearchStore';
import MiniTagsListProps from '../../../Types/MiniTagsListProps';
import styles from './MiniTagsList.module.css';

const MiniTagsList: React.FC<MiniTagsListProps> = ({ className }) => {
  const { setSearchString } = useSearchStore();
  const db = ['клубника', 'тест', 'молочка', 'на праздник'];

  const handleClick = (value: string) => {
    setSearchString(value);
  };

  return (
    <div className={classNames(styles.miniTagsListBox, className)}>
      {db.map((value, index) => (
        <>
          <p className={styles.miniTagText} onClick={() => handleClick(value)}>
            {value}
          </p>
        </>
      ))}
    </div>
  );
};

export default MiniTagsList;
