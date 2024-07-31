import { SearchBlockProps, SearchBlockValues } from '../../Types/types';
import BaseButton from '../Button/BaseButton/BaseButton';
import BaseField from '../Field/BaseField/BaseField';
import BaseForm from '../Form/BaseForm/BaseForm';
import styles from './SearchBlock.module.css';

const SearchBlock: React.FC<SearchBlockProps> = ({ text, className, onSubmit }) => {
  const initialValues: SearchBlockValues = {
    name: '',
  };
  return (
    <>
      <BaseForm initialValues={initialValues} onSubmit={onSubmit}>
        <div className={styles.searchBlock}>
          {text ? <h3>Поиск рецепта</h3> : undefined}
          <BaseField name="name" className={styles.serchFormField} placeholder="Название блюда" />
          <BaseButton primary className={styles.searchFormButton} buttonText="Поиск" />
        </div>
      </BaseForm>
    </>
  );
};

export default SearchBlock;
