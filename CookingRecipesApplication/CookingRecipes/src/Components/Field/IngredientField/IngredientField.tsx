import React from 'react';
import { FieldArray, FieldArrayRenderProps } from 'formik';
import styles from './IngredientField.module.css';
import BaseField from '../BaseField/BaseField';
import closeIcon from '../../../resources/icons/close.svg';
import IngredientFieldProps from '../../../Types/IngredientFieldProps';
import BaseButton from '../../Button/BaseButton/BaseButton';
import plusImg from '../../../resources/icons/plus-orange.svg';

const IngredientField: React.FC<IngredientFieldProps> = ({ name }) => {
  const handlerCreateField = (arrayHelpers: FieldArrayRenderProps) => {
    arrayHelpers.push({ name: '', product: '' });
  };

  const handlerDeleteCurrentField = (arrayHelpers: FieldArrayRenderProps, index: number) => {
    if (index == 0) {
      return;
    }
    arrayHelpers.remove(index);
  };

  return (
    <>
      <FieldArray
        name={name}
        render={(arrayHelpers: FieldArrayRenderProps) => {
          const ingredients: Array<{
            name: string;
            product: string;
          }> = arrayHelpers.form.values[name] || [];
          return (
            <>
              {ingredients.map((ingredient: { name: string; product: string }, index: number) => (
                <div key={index} className={styles.ingredient}>
                  <div className={styles.ingredientButtonBox}>
                    <button type="button" onClick={() => handlerDeleteCurrentField(arrayHelpers, index)}>
                      <img src={closeIcon} alt="closeIcon" className={styles.ingredientCloseIcon} />
                    </button>
                  </div>
                  <div className={styles.ingredientBox}>
                    <BaseField
                      className={styles.inputIngredientNameFormSize}
                      margin
                      name={`${name}.${index}.name`}
                      type="text"
                      labelText="Заголовок для ингридиентов"
                    />
                    <BaseField
                      className={styles.textareaIngredientFormSize}
                      as="textarea"
                      name={`${name}.${index}.product`}
                      placeholder="Список подуктов для категории"
                    />
                  </div>
                </div>
              ))}
              <BaseButton
                buttonText="Добавить заголовок"
                className={styles.ingredientButton}
                onClick={() => handlerCreateField(arrayHelpers)}
              >
                <img src={plusImg} className={styles.plusIcon} />
              </BaseButton>
            </>
          );
        }}
      />
    </>
  );
};

export default IngredientField;
