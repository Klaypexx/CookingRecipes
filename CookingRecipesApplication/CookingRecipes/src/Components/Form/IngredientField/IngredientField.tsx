import React from 'react';
import { FieldArray, FieldArrayRenderProps } from 'formik';
import { IngredientFieldPropas } from '../../../Types/types';
import styles from './IngredientField.module.css';
import FormField from '../FormField/FormField';
import AddRecipeButton from '../../Button/AddRecipeButton/AddRecipeButton';
import closeIcon from '../../../resources/icons/close.svg';

const IngredientField: React.FC<IngredientFieldPropas> = ({ name }) => {
  return (
    <FieldArray
      name={name}
      render={(arrayHelpers: FieldArrayRenderProps) => {
        const ingredients = arrayHelpers.form.values[name] || [];
        return (
          <>
            <div className={styles.ingredientFormBlock}>
              <h4 className={styles.ingredientHeader}>Ингридиенты</h4>
              {ingredients.map((ingredient: { header: string; products: string }, index: number) => (
                <div key={index}>
                  <div className={styles.ingredientButtonBox}>
                    <button className={styles.ingredientCloseButton}>
                      <img src={closeIcon} alt="closeIcon" className={styles.ingredientCloseIcon} />
                    </button>
                  </div>
                  <div className={styles.ingredientBox}>
                    <FormField
                      className={styles.inputIngredientNameFormSize}
                      margin
                      name={`ingredient.${index}.header`}
                      type="text"
                      placeholder="Заголовок для ингридиентов"
                    />
                    <FormField
                      className={styles.textareaIngredientFormSize}
                      as="textarea"
                      name={`ingredient.${index}.products`}
                      placeholder="Список подуктов для категории"
                    />
                  </div>
                </div>
              ))}
              <AddRecipeButton className={styles.ingredientButton} />
            </div>
          </>
        );
      }}
    />
  );
};

export default IngredientField;
