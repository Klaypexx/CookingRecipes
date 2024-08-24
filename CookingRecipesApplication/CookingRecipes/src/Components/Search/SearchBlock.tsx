import SearchBlockProps from '../../Types/SearchBlockProps';
import SearchBlockValues from '../../Types/SearchBlockValues';
import BaseButton from '../Button/BaseButton/BaseButton';
import BaseField from '../Field/BaseField/BaseField';
import BaseForm from '../Form/BaseForm/BaseForm';
import styles from './SearchBlock.module.css';

const SearchBlock: React.FC<SearchBlockProps> = ({ text, className, onSubmit }) => {
  const initialValues: SearchBlockValues = {
    searchString: '',
  };
  return (
    <>
      <BaseForm initialValues={initialValues} onSubmit={onSubmit}>
        <div className={styles.searchBlock}>
          {text && <h3>Поиск рецепта</h3>}
          <BaseField name="searchString" className={styles.serchFormField} placeholder="Название блюда" />
          <BaseButton primary type="submit" className={styles.searchFormButton} buttonText="Поиск" />
        </div>
      </BaseForm>
    </>
  );
};

export default SearchBlock;
