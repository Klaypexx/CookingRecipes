import classNames from 'classnames';
import bookIcon from '../../../resources/icons/ic-menu.svg';
import cookIcon from '../../../resources/icons/ic-cook.svg';
import chefIcon from '../../../resources/icons/ic-chef.svg';
import feastIcon from '../../../resources/icons/ic-feast.svg';
import styles from './TagsList.module.css';
import TagsBlockListProps from '../../../Types/TagsBlockListProps';

const tagsData = [
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

const TagsBlockList: React.FC<TagsBlockListProps> = ({ text, onClick, className }) => {
  return (
    <div className={styles.tagListFlex}>
      {tagsData.map((tags, index) => (
        <div key={index} onClick={() => onClick(tags.header)} className={classNames(styles.tagListBox, className)}>
          <div className={styles.iconBox}>
            <img src={tags.icon} alt="icon" className={styles.icon} />
          </div>
          <h3>{tags.header}</h3>
          {text && <p className={styles.tagTextBase}>{tags.text}</p>}
        </div>
      ))}
    </div>
  );
};

export default TagsBlockList;
