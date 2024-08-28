import { FieldArray, FieldArrayRenderProps } from 'formik';
import BaseField from '../BaseField/BaseField';
import styles from './StepField.module.css';
import closeIcon from '../../../resources/icons/close.svg';
import StepFieldProps from '../../../Types/StepFieldProps';
import BaseButton from '../../Button/BaseButton/BaseButton';
import plusImg from '../../../resources/icons/plus-orange.svg';

const StepField: React.FC<StepFieldProps> = ({ name }) => {
  const handlerCreateField = (arrayHelpers: FieldArrayRenderProps) => {
    arrayHelpers.push({ description: '' }); // Ensure we push an object with a description
  };

  const handlerDeleteCurrentField = (arrayHelpers: FieldArrayRenderProps, index: number) => {
    if (index === 0) {
      return;
    }
    arrayHelpers.remove(index);
  };

  return (
    <FieldArray
      name={name}
      render={(arrayHelpers: FieldArrayRenderProps) => {
        const steps: Array<{
          description: string;
        }> = arrayHelpers.form.values[name] || [];
        return (
          <>
            {steps.map((step: { description: string }, index: number) => (
              <div key={index}>
                <div className={styles.step}>
                  <div className={styles.stepBox}>
                    <p className={styles.stepText}>Шаг {index + 1}</p>
                    <button type="button" onClick={() => handlerDeleteCurrentField(arrayHelpers, index)}>
                      <img src={closeIcon} alt="closeIcon" className={styles.stepCloseIcon} />
                    </button>
                  </div>
                  <BaseField
                    className={styles.inputStepFormSize}
                    margin
                    name={`${name}.${index}.description`}
                    as="textarea"
                    placeholder="Описание шага"
                  />
                </div>
              </div>
            ))}
            <div className={styles.stepButtonBox}>
              <BaseButton
                buttonText="Добавить шаг"
                className={styles.stepButton}
                onClick={() => handlerCreateField(arrayHelpers)}
              >
                <img src={plusImg} className={styles.plusIcon} />
              </BaseButton>
            </div>
          </>
        );
      }}
    />
  );
};

export default StepField;
