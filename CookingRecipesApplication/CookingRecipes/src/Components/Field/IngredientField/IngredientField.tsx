import React from 'react';
import { FieldArray, FieldArrayRenderProps } from 'formik';
import { IngredientFieldProps } from '../../../Types/types';
import styles from './IngredientField.module.css';
import BaseField from '../BaseField/BaseField';
import AddRecipeButton from '../../Button/AddRecipeButton/AddRecipeButton';
import closeIcon from '../../../resources/icons/close.svg';

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
          const ingredients = arrayHelpers.form.values[name] || [];
          return (
            <>
              {ingredients.map((ingredient: { name: string; product: string }, index: number) => (
                <div key={index} className={styles.ingredientContainer}>
                  <div className={styles.ingredientButtonBox}>
                    <button
                      type="button"
                      className={styles.ingredientCloseButton}
                      onClick={() => handlerDeleteCurrentField(arrayHelpers, index)}
                    >
                      <img src={closeIcon} alt="closeIcon" className={styles.ingredientCloseIcon} />
                    </button>
                  </div>
                  <div className={styles.ingredientBox}>
                    <BaseField
                      className={styles.inputIngredientNameFormSize}
                      margin
                      name={`${name}.${index}.name`}
                      type="text"
                      placeholder="Заголовок для ингридиентов"
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
              <AddRecipeButton
                buttonText="Добавить заголовок"
                className={styles.ingredientButton}
                onClick={() => handlerCreateField(arrayHelpers)}
              />
            </>
          );
        }}
      />
    </>
  );
};

export default IngredientField;
