import BaseField from '../BaseField/BaseField';
import TagsField from '../TagsField/TagsField';
import styles from './CardField.module.css';

const CardField = () => {
  return (
    <div className={styles.cardFormContainer}>
      <BaseField
        className={styles.inputRecipeNameFormSize}
        margin
        name="name"
        type="text"
        placeholder="Название рецепта"
      />
      <BaseField
        margin
        className={styles.textareaFormSize}
        as="textarea"
        name="description"
        placeholder="Краткое описание рецепта (150 символов)"
        maxLength={150}
      />
      <TagsField name="tags" />
      <div className={styles.optionContainer}>
        <div className={styles.optionBox}>
          <BaseField select as="select" name="cookingTime" className={styles.optionForm}>
            <option value="">Время готовки</option>
            <option value="5">5</option>
            <option value="10">10</option>
            <option value="15">15</option>
            <option value="30">30</option>
            <option value="35">35</option>
            <option value="40">40</option>
            <option value="60">60</option>
          </BaseField>
          <p className={styles.optionText}>Минут</p>
        </div>
        <div className={styles.optionBox}>
          <BaseField select as="select" name="portion" className={styles.optionForm}>
            <option value="">Порций в блюде</option>
            <option value="1">1</option>
            <option value="2">2</option>
            <option value="3">3</option>
            <option value="4">4</option>
            <option value="5">5</option>
          </BaseField>
          <p className={styles.optionText}>Персон</p>
        </div>
      </div>
    </div>
  );
};

export default CardField;
