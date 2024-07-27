import { FieldArray, FieldArrayRenderProps, setIn } from 'formik';
import BaseField from '../BaseField/BaseField';
import styles from './StepField.module.css';
import AddRecipeButton from '../../Button/AddRecipeButton/AddRecipeButton';
import closeIcon from '../../../resources/icons/close.svg';
import { StepFieldProps } from '../../../Types/types';
import { useState } from 'react';

const StepField: React.FC<StepFieldProps> = ({ name }) => {
  const handlerCreateField = (arrayHelpers: FieldArrayRenderProps) => {
    arrayHelpers.push('');
  };

  const handlerDeleteCurrentField = (arrayHelpers: FieldArrayRenderProps, index: number) => {
    if (index == 0) {
      return;
    }
    arrayHelpers.remove(index);
  };

  return (
    <FieldArray
      name={name}
      render={(arrayHelpers: FieldArrayRenderProps) => {
        const steps = arrayHelpers.form.values[name] || [];
        return (
          <>
            {steps.map((step: [], index: number) => (
              <div key={index}>
                <div className={styles.stepContainer}>
                  <div className={styles.stepBox}>
                    <p className={styles.stepText}>Шаг {index + 1}</p>
                    <button type="button" onClick={() => handlerDeleteCurrentField(arrayHelpers, index)}>
                      <img src={closeIcon} alt="closeIcon" className={styles.stepCloseIcon} />
                    </button>
                  </div>
                  <BaseField
                    className={styles.inputStepFormSize}
                    margin
                    name={`${name}.${index}`}
                    as="textarea"
                    placeholder="Описание шага"
                  />
                </div>
              </div>
            ))}
            <div className={styles.stepButtonBox}>
              <AddRecipeButton
                buttonText="Добавить шаг"
                className={styles.stepButton}
                onClick={() => handlerCreateField(arrayHelpers)}
              />
            </div>
          </>
        );
      }}
    />
  );
};

export default StepField;
