import { useNavigate } from 'react-router-dom';
import chefIcon from '../../../resources/icons/ic-chef.svg';
import cookIcon from '../../../resources/icons/ic-cook.svg';
import feastIcon from '../../../resources/icons/ic-feast.svg';
import bookIcon from '../../../resources/icons/ic-menu.svg';
import useSearchStore from '../../../Stores/useSearchStore';
import TagsListProps from '../../../Types/TagsListProps';
import BaseTagsBlock from '../BaseTagsBlock/BaseTagsBlock';
import styles from './TagsList.module.css';

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

const TagsList: React.FC<TagsListProps> = ({ text, className }) => {
  const navigation = useNavigate();
  const { setSearchString } = useSearchStore();

  const handleClick = (value: string) => {
    setSearchString(value);
    navigation('/recipes');
  };

  return (
    <div className={styles.tagListFlex}>
      {tagsData.map((tags, index) => (
        <div key={index} onClick={() => handleClick(tagsData[index].header)}>
          <BaseTagsBlock text={text} className={className} tag={tags} />
        </div>
      ))}
    </div>
  );
};

export default TagsList;
