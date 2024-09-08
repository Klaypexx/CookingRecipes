import { useEffect, useState } from 'react';
import useSearchStore from '../../../Stores/useSearchStore';
import SearchBlockValues from '../../../Types/SearchBlockValues';
import SearchFormProps from '../../../Types/SearchFormProps';
import BaseButton from '../../Button/BaseButton/BaseButton';
import BaseField from '../../Field/BaseField/BaseField';
import BaseForm from '../BaseForm/BaseForm';
import styles from './SearchForm.module.css';

const SearchForm: React.FC<SearchFormProps> = ({ text, onSubmit }) => {
  const { searchString } = useSearchStore();
  const [initialValues, setInitialValues] = useState<SearchBlockValues>({ searchString: searchString });

  useEffect(() => {
    setInitialValues({ searchString: searchString });
  }, [searchString]);

  return (
    <BaseForm initialValues={initialValues} onSubmit={onSubmit}>
      <div className={styles.searchBox}>
        {text && <h3 className={styles.searchFormText}>Поиск рецепта</h3>}
        <BaseField name="searchString" className={styles.serchFormField} placeholder="Название блюда..." />
        <BaseButton primary type="submit" className={styles.searchFormButton} buttonText="Поиск" />
      </div>
    </BaseForm>
  );
};

export default SearchForm;
