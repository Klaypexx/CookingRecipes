import classNames from 'classnames';
import bookIcon from '../../../resources/icons/ic-menu.svg';
import cookIcon from '../../../resources/icons/ic-cook.svg';
import chefIcon from '../../../resources/icons/ic-chef.svg';
import feastIcon from '../../../resources/icons/ic-feast.svg';
import styles from './TagsList.module.css';
import { TagsBlockListProps } from '../../../Types/types';

const tagsBd = [
  {
    icon: bookIcon,
    header: 'Простые блюда',
    text: 'Время приготвления таких блюд не более 1 часа',
  },
  {
    icon: cookIcon,
    header: 'Детское',
    text: 'Самые полезные блюда которые можно детям любого возраста',
  },
  {
    icon: chefIcon,
    header: 'От шеф-поваров',
    text: 'Требуют умения, времени и терпения, зато как в ресторане',
  },
  {
    icon: feastIcon,
    header: 'На праздник',
    text: 'Чем удивить гостей, чтобы все были сыты за праздничным столом',
  },
];

const TagsBlockList: React.FC<TagsBlockListProps> = ({ text, className }) => {
  return (
    <div className={styles.tagListContainer}>
      {tagsBd.map((tags) => (
        <>
          <div className={classNames(styles.tagListBlockBase, className)}>
            <div className={styles.iconBlock}>
              <img src={tags.icon} alt="photo" className={styles.icon} />
            </div>
            <h3>{tags.header}</h3>
            {text ? <p>{tags.text}</p> : undefined}
          </div>
        </>
      ))}
    </div>
  );
};

export default TagsBlockList;
