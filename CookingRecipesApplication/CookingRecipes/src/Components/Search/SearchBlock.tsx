import useSearchStore from '../../Stores/useSearchStore';
import SearchBlockProps from '../../Types/SearchBlockProps';
import SearchBlockValues from '../../Types/SearchBlockValues';
import BaseButton from '../Button/BaseButton/BaseButton';
import BaseField from '../Field/BaseField/BaseField';
import BaseForm from '../Form/BaseForm/BaseForm';
import styles from './SearchBlock.module.css';

const SearchBlock: React.FC<SearchBlockProps> = ({ text, onSubmit }) => {
  const { searchString } = useSearchStore();

  const initialValues: SearchBlockValues = {
    searchString: searchString,
  };

  return (
    <>
      <BaseForm initialValues={initialValues} onSubmit={onSubmit}>
        <div className={styles.searchBox}>
          {text && <h3 className={styles.searchFormText}>Поиск рецепта</h3>}
          <BaseField name="searchString" className={styles.serchFormField} placeholder="Название блюда" />
          <BaseButton primary type="submit" className={styles.searchFormButton} buttonText="Поиск" />
        </div>
      </BaseForm>
    </>
  );
};

export default SearchBlock;
